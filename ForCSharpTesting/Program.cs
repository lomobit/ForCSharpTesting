using System.Drawing.Imaging;
using System.Drawing;

namespace ForCSharpTesting;

public class Program
{
    static void Main(string[] args)
    {
        //BitmapsWithArrayPoolAndWithout.StartBenchmark();

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

        Console.WriteLine($"TotalMemory_before: {GC.GetTotalMemory(true)}");

        for (int ii = 0; ii < 1000; ii++)
        {

            if (ii % 10 == 0) Console.WriteLine(ii);
            var filePath = ii % 2 == 0 ? smallFilePath : largeFilePath;

            // Загрузка файла в Stream
            using (FileStream readerStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Преобразование Stream в Image
                using (Image image = Image.FromStream(readerStream))
                {

                    for (int i = 0; i < sizes.Length; i++)
                    {
                        var size = sizes[i];
                        if (size > image.Width)
                        {
                            break;
                        }

                        // Изменение размера изображения
                        using (var bitmap = new Bitmap(image, new Size(size, size * image.Height / image.Width)))
                        {
                            //var imageStream = new ImageStream(bitmap);

                            // Сохранение в формате JPEG
                            string outputFilePath = @$"{path}\new\{(ii % 2 == 0 ? "small" : "large")}_image_{size}.jpg";
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                bitmap.Save(memoryStream, ImageFormat.Jpeg);
                                memoryStream.Position = 0;

                                using (FileStream writerStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
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

        Console.WriteLine($"TotalMemory_final: {GC.GetTotalMemory(true)}");

        Console.ReadKey();
    }
}