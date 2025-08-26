from fastapi import FastAPI, File, UploadFile
from fastapi.responses import JSONResponse
import tensorflow as tf
import tensorflow_hub as hub
from tensorflow.keras import layers, Model, regularizers
from PIL import Image
import numpy as np
import io
import os
from tensorflow.keras.preprocessing import image

app = FastAPI()

# --------- Custom Layers ---------
class SEBlock(layers.Layer):
    def __init__(self, ratio=16, **kwargs):
        super(SEBlock, self).__init__(**kwargs)
        self.ratio = ratio

    def build(self, input_shape):
        self.channels = input_shape[-1]
        self.global_pool = layers.GlobalAveragePooling2D()
        self.fc1 = layers.Dense(self.channels // self.ratio, activation='swish')
        self.fc2 = layers.Dense(self.channels, activation='sigmoid')
        self.reshape = layers.Reshape((1, 1, self.channels))

    def call(self, inputs):
        se = self.global_pool(inputs)
        se = self.fc1(se)
        se = self.fc2(se)
        se = self.reshape(se)
        return inputs * se

class Avg2MaxPooling(layers.Layer):
    def __init__(self, pool_size=3, strides=2, padding='same', **kwargs):
        super(Avg2MaxPooling, self).__init__(**kwargs)
        self.avg_pool = layers.AveragePooling2D(pool_size, strides, padding)
        self.max_pool = layers.MaxPooling2D(pool_size, strides, padding)
        self.bn = layers.BatchNormalization()

    def call(self, inputs):
        x = self.avg_pool(inputs) - 2 * self.max_pool(inputs)
        return self.bn(x)

class DepthwiseSeparableConv(layers.Layer):
    def __init__(self, filters, kernel_size=3, strides=1, se_ratio=16, reg=0.001, **kwargs):
        super(DepthwiseSeparableConv, self).__init__(**kwargs)
        self.dw = layers.DepthwiseConv2D(kernel_size, strides, padding='same',
                                         depthwise_regularizer=regularizers.l2(reg))
        self.pw = layers.Conv2D(filters, 1, strides=1, kernel_regularizer=regularizers.l2(reg))
        self.bn = layers.BatchNormalization()
        self.se = SEBlock(se_ratio)
        self.proj = layers.Conv2D(filters, 1, strides=1,
                                  kernel_regularizer=regularizers.l2(reg)) if strides != 1 else None

    def call(self, inputs):
        residual = inputs
        x = self.dw(inputs)
        x = self.pw(x)
        x = self.bn(x)
        x = tf.nn.swish(x)
        x = self.se(x)
        if self.proj is not None:
            residual = self.proj(residual)
        return x + residual if residual.shape == x.shape else x

# --------- Load the model with all custom objects ---------
model_path = os.path.join(os.path.dirname(__file__), "thyroid_cancer_model.h5")


model = tf.keras.models.load_model(
    model_path,
    compile = False,
    custom_objects={
        "KerasLayer": hub.KerasLayer,
        "SEBlock": SEBlock,
        "Avg2MaxPooling": Avg2MaxPooling,
        "DepthwiseSeparableConv": DepthwiseSeparableConv
    }
)

# --------- Image Preprocessing ---------
IMG_SIZE = (224, 224)

def preprocess_image(image_bytes):
    image = Image.open(io.BytesIO(image_bytes)).convert("RGB")
    image = image.resize(IMG_SIZE)
    image_array = np.array(image).astype("float32") / 255.0
    image_array = np.expand_dims(image_array, axis=0)
    return image_array

# --------- Prediction Endpoint ---------
@app.post("/predict")
async def predict(file: UploadFile = File(...)):
    try:
        contents = await file.read()
        image = preprocess_image(contents)
        prediction = model.predict(image)

        prediction_value = float(prediction[0][0])
        label = "malignant" if prediction_value > 0.5 else "benign"

        return JSONResponse({
            "label": label,
            "prediction_value": prediction_value
        })

    except Exception as e:
        return JSONResponse(
            status_code=500,
            content={"error": str(e)}
        )
