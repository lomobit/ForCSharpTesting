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
    public void WithArrayPool()
    {
        var pool = ArrayPool<Bitmap>.Shared;
        using (FileStream readerStream = new FileStream(Path + TestFile, FileMode.Open, FileAccess.Read))
        {
            using (Image image = Image.FromStream(readerStream))
            {
                var bitmapArr = pool.Rent(Sizes.Length);

                for (int i = 0; i < Sizes.Length; i++)
                {
                    var size = Sizes[i];
                    if (size > image.Width)
                    {
                        break;
                    }


                    // Изменение размера изображения
                    using (bitmapArr[i] = new Bitmap(image, new Size(size, size * image.Height / image.Width)))
                    { 
                        // Сохранение в формате JPEG
                        string outputFilePath = @$"{Path}\new\{TestFile}_arrayPool_image_{size}.jpg";
                        using (FileStream writerStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                        {
                            bitmapArr[i].Save(writerStream, ImageFormat.Jpeg);
                        }

                    }
                }

                pool.Return(bitmapArr);

            }
        }
    }

    [Benchmark]
    public void WithoutArrayPool()
    {
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
                        using (FileStream writerStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                        {
                            bitmap.Save(writerStream, ImageFormat.Jpeg);
                        }
                    }
                }
            }
        }
    }
}