using System.Drawing;
using Yolov5Net.Scorer;
using Yolov5Net.Scorer.Models;

internal class Program
{
    static readonly string assetsPath = GetAbsolutePath(@"../../../assets");
    static readonly string modelFilePath = Path.Combine(assetsPath, "Model", "yolov7.onnx");
    static readonly string imagesFolder = Path.Combine(assetsPath, "images");
    static readonly string outputFolder = Path.Combine(assetsPath, "images", "output");

    private static void Main(string[] args)
    {
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }
        var imgs = Directory.GetFiles(imagesFolder).Where(filePath => Path.GetExtension(filePath) == ".jpg");

        using var scorer = new YoloScorer<YoloCocoP5Model>(modelFilePath);

        foreach (var imgsFile in imgs)
        {
            using var image = Image.FromFile(imgsFile);
            List<YoloPrediction> predictions = scorer.Predict(image);
            using var graphics = Graphics.FromImage(image);
            foreach (var prediction in predictions)
            {
                double score = Math.Round(prediction.Score, 2);

                graphics.DrawRectangles(new Pen(prediction.Label.Color, 1),
                    new[] { prediction.Rectangle });

                var (x, y) = (prediction.Rectangle.X - 3, prediction.Rectangle.Y - 23);

                graphics.DrawString($"{prediction.Label.Name} ({score})",
                    new Font("Consolas", 16, GraphicsUnit.Pixel), new SolidBrush(prediction.Label.Color),
                    new PointF(x, y));
            }

            image.Save(Path.Combine(outputFolder, $"result{DateTime.Now.Ticks}.jpg"));
        }
    }

    /// <summary>
    /// 获取程序启动目录
    /// </summary>
    /// <param name="relativePath">之下的某个路径</param>
    /// <returns></returns>
    private static string GetAbsolutePath(string relativePath)
    {
        FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
        string assemblyFolderPath = _dataRoot.Directory!.FullName;

        string fullPath = Path.Combine(assemblyFolderPath, relativePath);

        return fullPath;
    }

}

