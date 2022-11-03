using Microsoft.ML;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

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

