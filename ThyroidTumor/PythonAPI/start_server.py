import uvicorn

if __name__ == "__main__":
    uvicorn.run("ModelAPI:app", host="127.0.0.1", port=8000)
