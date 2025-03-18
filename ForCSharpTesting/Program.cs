using System;
using System.Buffers;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Pipes;
using ForCSharpTesting.LeetCodeSolutions.Common;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftAntimalwareEngine;

namespace ForCSharpTesting;

public class Program
{
    static void Main(string[] args)
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

        var pool = ArrayPool<Bitmap>.Shared;

        Console.WriteLine($"TotalMemory_before: {GC.GetTotalMemory(true)}");
        Image image = Image.FromFile(smallFilePath);

        for (int ii = 0; ii < 100; ii++)
        {
            if (ii % 10 == 0) Console.WriteLine(ii);
            var filePath = ii % 2 == 0 ? smallFilePath : largeFilePath;

            // Загрузка файла в Stream
            using (FileStream readerStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {

                //Console.WriteLine($"TotalMemory_fileStream: {GC.GetTotalMemory(true)}");

                // Преобразование Stream в Image
                //using (Image image = Image.FromStream(fileStream))
                //{
                //Console.WriteLine($"TotalAllocatedBytes_image: {GC.GetTotalAllocatedBytes()}");
                //Console.WriteLine($"TotalMemory_image: {GC.GetTotalMemory(true)}");
                var bitmapArr = pool.Rent(sizes.Length);

                for (int i = 0; i < sizes.Length; i++)
                {
                    var size = sizes[i];
                    if (size > image.Width)
                    {
                        break;
                    }


                    //var bitmapArr = new Bitmap[1];

                    // Изменение размера изображения
                    using (bitmapArr[i] = new Bitmap(image, new Size(size, size * image.Height / image.Width)))
                    {

                        //Console.WriteLine($"\nTotalMemory_newBitmap_{size}: {GC.GetTotalMemory(true)}");

                        // Сохранение в формате JPEG
                        string outputFilePath = @$"{path}\new\{(ii % 2 == 0 ? "small" : "large")}_image_{size}.jpg";
                        using (FileStream writerStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                        {
                            bitmapArr[i].Save(writerStream, ImageFormat.Jpeg);
                        }

                    }
                }

                pool.Return(bitmapArr);

                //}
            }
        }

        Console.WriteLine($"TotalMemory_final: {GC.GetTotalMemory(true)}");
    }
}