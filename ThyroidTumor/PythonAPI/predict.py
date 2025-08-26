import sys
import tensorflow as tf
import tensorflow_hub as hub
import tf_keras
from PIL import Image
import numpy as np
import os
import json
import logging
import contextlib
import io

os.environ['TF_CPP_MIN_LOG_LEVEL'] = '3'  # Disable TF C++ backend logs
tf.get_logger().setLevel(logging.ERROR)  # Disable training logs


model_path = os.path.join(os.path.dirname(__file__), "thyroid_cancer_model.h5")
model = tf.keras.models.load_model(model_path, custom_objects = {
        "KerasLayer" : hub.KerasLayer
    })

IMG_SIZE = (224, 224)

def preprocess_image(image_path):
    image = Image.open(image_path).convert("RGB")
    image = image.resize(IMG_SIZE)
    image_array = np.array(image).astype("float32")
    image_array = np.expand_dims(image_array, axis = 0)
    return image_array

def main():
    if len(sys.argv) < 2:
        print("Error: No image path provided.")
        sys.exit(1)

    image_path = sys.argv[1]
    image = preprocess_image(image_path)

    with contextlib.redirect_stdout(io.StringIO()):
        prediction = model.predict(image)

    label = "malignant" if prediction[0][0] > 0.5 else "benign"
    confidence = float(prediction[0][0])
    result = {"label": label, "confidence": confidence}
    print(json.dumps(result))

if __name__ == "__main__":
    main()