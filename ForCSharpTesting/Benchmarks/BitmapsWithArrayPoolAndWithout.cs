using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using System.Buffers;
using System.Drawing.Imaging;
using System.Drawing;
using BenchmarkDotNet.Running;

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
    public void WithArrayPoolWithKiBBuffer()
    {
        var pool = ArrayPool<byte>.Shared;
        byte[] smallBuffer = pool.Rent(1024);

        try
        {
            // Загрузка файла в Stream
            using (FileStream readerStream = new FileStream(Path + TestFile, FileMode.Open, FileAccess.Read))
            {
                // Преобразование Stream в Image
                using (Image image = Image.FromStream(readerStream))
                {

                    for (int i = 0; i < Sizes.Length; i++)
                    {
                        var size = Sizes[i];
                        if (size > image.Width)
                        {
                            break;
                        }

                        // Изменение размера изображения
                        using (var bitmap = new Bitmap(image, new Size(size, size * image.Height / image.Width)))
                        { 
                            // Сохранение в формате JPEG
                            string outputFilePath = @$"{Path}\new\{TestFile}_withoutArrayPool_image_{size}.jpg";
                            using (MemoryStream memoryStream = new MemoryStream(smallBuffer))
                            {
                                bitmap.Save(memoryStream, ImageFormat.Jpeg); 
                                memoryStream.Position = 0;

                                using (FileStream writerStream =
                                       new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                                {
                                    // Копирование данных из MemoryStream в FileStream
                                    memoryStream.CopyTo(writerStream);
                                }
                            }
                        }
                    }
                }
            }
        }
        finally
        {
            pool.Return(smallBuffer);
        }
    }

    [Benchmark]
    public void WithArrayPoolWithMiBBuffer()
    {
        var pool = ArrayPool<byte>.Shared;
        byte[] smallBuffer = pool.Rent(1024*1024);

        try
        {
            // Загрузка файла в Stream
            using (FileStream readerStream = new FileStream(Path + TestFile, FileMode.Open, FileAccess.Read))
            {
                // Преобразование Stream в Image
                using (Image image = Image.FromStream(readerStream))
                {

                    for (int i = 0; i < Sizes.Length; i++)
                    {
                        var size = Sizes[i];
                        if (size > image.Width)
                        {
                            break;
                        }

                        // Изменение размера изображения
                        using (var bitmap = new Bitmap(image, new Size(size, size * image.Height / image.Width)))
                        {
                            // Сохранение в формате JPEG
                            string outputFilePath = @$"{Path}\new\{TestFile}_withoutArrayPool_image_{size}.jpg";
                            using (MemoryStream memoryStream = new MemoryStream(smallBuffer))
                            {
                                bitmap.Save(memoryStream, ImageFormat.Jpeg);
                                memoryStream.Position = 0;

                                using (FileStream writerStream =
                                       new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                                {
                                    // Копирование данных из MemoryStream в FileStream
                                    memoryStream.CopyTo(writerStream);
                                }
                            }
                        }
                    }
                }
            }
        }
        finally
        {
            pool.Return(smallBuffer);
        }
    }

    [Benchmark]
    public void WithoutArrayPool()
    {
        // Загрузка файла в Stream
        using (FileStream readerStream = new FileStream(Path + TestFile, FileMode.Open, FileAccess.Read))
        {
            // Преобразование Stream в Image
            using (Image image = Image.FromStream(readerStream))
            {

                for (int i = 0; i < Sizes.Length; i++)
                {
                    var size = Sizes[i];
                    if (size > image.Width)
                    {
                        break;
                    }

                    // Изменение размера изображения
                    using (var bitmap = new Bitmap(image, new Size(size, size * image.Height / image.Width)))
                    {
                        // Сохранение в формате JPEG
                        string outputFilePath = @$"{Path}\new\{TestFile}_withoutArrayPool_image_{size}.jpg";
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            bitmap.Save(memoryStream, ImageFormat.Jpeg);
                            memoryStream.Position = 0;

                            using (FileStream writerStream =
                                   new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                            {
                                // Копирование данных из MemoryStream в FileStream
                                memoryStream.CopyTo(writerStream);
                            }
                        }
                    }
                }
            }
        }
    }
}