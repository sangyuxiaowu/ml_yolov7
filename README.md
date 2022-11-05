# ml_yolov7

ML.NET Yolov7

模型文件 [yolov7.onnx](https://download.csdn.net/download/marin1993/86912472) 

来自项目 https://github.com/WongKinYiu/yolov7 的 `yolov7.pt` 导出，原项目 releases v0.1 提供的无法使用，需自行调整参数导出。

```bash
python .\export.py --weights=yolov7.pt --grid --simplify
```