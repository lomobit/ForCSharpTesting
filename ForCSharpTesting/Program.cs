using ForCSharpTesting.Benchmarks;
using Microsoft.Diagnostics.Runtime.Utilities;
using SkiaSharp;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Xunit.Sdk;

namespace ForCSharpTesting;

public class Program
{
    const int countTasks = 1;
    const int countIterations = 2;

    static bool needToCount = true;

    static int count = 0;

    static List<int> list = new List<int>(4 * 60); // 4 минуты

    public static void Main(string[] args)
    {
        //BitmapsWithArrayPoolAndWithout.StartBenchmark();

        //GCNotiofication.StartGCCheck();

        Thread jobPerSecondCounter = new Thread(Count);
        jobPerSecondCounter.Start();

        Console.WriteLine($"TotalMemory_before: {GC.GetTotalMemory(true)}");

        

        var tasks = new List<Task>(countTasks);
        for (int i = 0; i < countTasks; i++)
        {
            tasks.Add(Task.Run(SkiaSharp));
        }

        Task.WhenAll(tasks).GetAwaiter().GetResult();

        needToCount = false;

        //Console.WriteLine(list.Sum() / list.Count);
        Console.WriteLine(count);

        Console.WriteLine($"TotalMemory_final: {GC.GetTotalMemory(true)}");

        //GCNotiofication.StopGCCheck();

        Console.ReadKey();
    }

    public static void Count()
    {
        while (needToCount)
        {
            Thread.Sleep(1000);
            list.Add(count);
        }
    }

    public static async Task SkiaSharp()
    {
        var path = @"D:\Projects\Adve\Code\adve-community-ui\public\images\mocks\location-bridges\";
        var smallFilePath = @"D:\Projects\Adve\Code\adve-community-ui\public\images\mocks\location-bridges\bridge1.png";
        var largeFilePath = @"D:\Projects\Adve\Code\adve-community-ui\public\images\mocks\location-bridges\bridge4.png";

        int[] mobileSizes = { 320, 375, 425, 480 };
        int[] tabletSizes = { 640, 768, 960, 1024 };
        int[] notebookSizes = { 1280, 1366, 1440, 1600, 1920 };

        var sizes = mobileSizes
            .Concat(tabletSizes)
            .Concat(notebookSizes)
            .ToArray();

        var consoleInfo = false;
        for (int ii = 0; ii < countIterations; ii++)
        {
            count++;

            //if (ii % 10 == 0) Console.WriteLine(ii);
            var filePath = ii % 2 == 0 ? smallFilePath : largeFilePath;

            if (consoleInfo) Console.WriteLine($"TotalMemory_1: {GC.GetTotalMemory(true)}");
            // Загрузка файла в Stream
            using FileStream readerStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);


            if (consoleInfo) Console.WriteLine($"TotalMemory_2: {GC.GetTotalMemory(true)}");
            // Преобразование Stream в Image
            using var bitmap = SKBitmap.Decode(readerStream);

            readerStream.Dispose();



            for (int i = 0; i < sizes.Length; i++)
            {
                var size = sizes[i];
                if (size > bitmap.Width)
                {
                    break;
                }

                if (consoleInfo) Console.WriteLine();
                if (consoleInfo) Console.WriteLine($"TotalMemory_3_{i}: {GC.GetTotalMemory(true)}");
                var opt = new SKSamplingOptions(SKFilterMode.Linear, SKMipmapMode.Linear);
                using var resizedBitmap = bitmap.Resize(new SKImageInfo(size, size * bitmap.Height / bitmap.Width), opt);

                if (consoleInfo) Console.WriteLine($"TotalMemory_4_{i}: {GC.GetTotalMemory(true)}");
                // Сохранение в формате JPEG
                //string outputFilePath = @$"{path}\new\{(ii % 2 == 0 ? "small" : "large")}_{Thread.CurrentThread.ManagedThreadId}_{DateTime.Now.Ticks}_image_{size}.jpg";
                string outputFilePath = @$"{path}\new\{(ii % 2 == 0 ? "small" : "large")}_image_{size}.jpg";

                if (consoleInfo) Console.WriteLine($"TotalMemory_5_{i}: {GC.GetTotalMemory(true)}");

                using var data = resizedBitmap.Encode(SKEncodedImageFormat.Jpeg, 85);

                if (consoleInfo) Console.WriteLine($"TotalMemory_8_{i}: {GC.GetTotalMemory(true)}");

                if (consoleInfo) Console.WriteLine($"TotalMemory_9_{i}: {GC.GetTotalMemory(true)}");
                using FileStream writerStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);

                if (consoleInfo) Console.WriteLine($"TotalMemory_10_{i}: {GC.GetTotalMemory(true)}");
                // Копирование данных из MemoryStream в FileStream

                using var dataStream = data.AsStream();
                //using var dataStream = new AdveSKDataReadOnlyStream(data);
                await dataStream.CopyToAsync(writerStream);
                if (consoleInfo) Console.WriteLine();

                dataStream.Dispose();
                writerStream.Dispose();
                //File.Delete(outputFilePath);
            }

            count--;
            await Task.Delay(100);
        }

        //GC.Collect(GC.MaxGeneration, GCCollectionMode.Aggressive);
    }
}

