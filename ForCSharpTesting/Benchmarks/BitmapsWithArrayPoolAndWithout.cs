using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Running;
using SkiaSharp;
using Microsoft.Maui.Graphics;

namespace ForCSharpTesting.Benchmarks;

[MemoryDiagnoser]
[InliningDiagnoser(true, true)]
[TailCallDiagnoser]
[EtwProfiler]
[ConcurrencyVisualizerProfiler]
[NativeMemoryProfiler]
//[ThreadingDiagnoser]
public class BitmapsWithArrayPoolAndWithout
{
    [Params(
        "bridge1.png",
        "bridge4.png"
    )]
    public string TestFile;

    public string Path = @"D:\Projects\Adve\Code\adve-community-ui\public\images\mocks\location-bridges\";

    public int[] Sizes = [320, 375, 425, 480, 640, 768, 960, 1024, 1280, 1366, 1440, 1600, 1920];

    public static void StartBenchmark()
    {
        BenchmarkRunner.Run<BitmapsWithArrayPoolAndWithout>();
    }

    [Benchmark]
    public void SkiaSharp()
    {
        // Загрузка файла в Stream
        using FileStream readerStream = new FileStream(Path + TestFile, FileMode.Open, FileAccess.Read);
        // Преобразование Stream в Image
        using var bitmap = SKBitmap.Decode(readerStream);
        for (int i = 0; i < Sizes.Length; i++)
        {
            var size = Sizes[i];
            if (size > bitmap.Width)
            {
                break;
            }

            using var resizedBitmap = bitmap.Resize(new SKImageInfo(size, size * bitmap.Height / bitmap.Width), default(SKSamplingOptions));
            // Сохранение в формате JPEG
            string outputFilePath = @$"{Path}\new\{TestFile}_SkiaSharp_image_{size}.jpg";
            using var image = SKImage.FromBitmap(resizedBitmap);
            using MemoryStream memoryStream = new MemoryStream();
            using var data = image.Encode(SKEncodedImageFormat.Jpeg, 80);
            data.SaveTo(memoryStream);

            memoryStream.Position = 0;

            using FileStream writerStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);
            // Копирование данных из MemoryStream в FileStream
            memoryStream.CopyTo(writerStream);
        }
    }

    [Benchmark]
    public void SystemDrawing()
    {
        // Загрузка файла в Stream
        using FileStream readerStream = new FileStream(Path + TestFile, FileMode.Open, FileAccess.Read);
        // Преобразование Stream в Image
        using var image = System.Drawing.Image.FromStream(readerStream);
        for (int i = 0; i < Sizes.Length; i++)
        {
            var size = Sizes[i];
            if (size > image.Width)
            {
                break;
            }

            // Изменение размера изображения
            using var bitmap = new System.Drawing.Bitmap(image, new System.Drawing.Size(size, size * image.Height / image.Width));
            // Сохранение в формате JPEG
            string outputFilePath = @$"{Path}\new\{TestFile}_SystemDrawing_image_{size}.jpg";
            using MemoryStream memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            memoryStream.Position = 0;

            using FileStream writerStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);
            // Копирование данных из MemoryStream в FileStream
            memoryStream.CopyTo(writerStream);
        }
    }
}