using System.Net.Http.Headers;

namespace ForCSharpTesting.HighLoadTesting;

public static class HighLoadTesting
{
    private const int Iterations = 100;
    private const int TasksCount = 10;

    public static async Task HighLoadFilesMultiThread()
    {
        var tasks = new List<Task>(TasksCount);
        for (int i = 0; i < TasksCount; i++)
        {
            var task = new Task(HighLoadFiles);
            tasks.Add(task);

            task.Start();
        }

        await Task.WhenAll(tasks);
    }

    public static void HighLoadFiles()
    {
        var title = "Название локации";
        var directoryPath = @"D:\Projects\Adve\Code\adve-community-ui\public\images\mocks\location-bridges\";
        string[] fileNames =
        [
            "2000.png",
            "bridge1.png",
            "bridge2.png",
            "bridge3.png",
            "bridge4.png",
            "titlePhoto.png",
        ];


        for (int i = 0; i < Iterations; i++)
        {
            using var client = new HttpClient();

            foreach (var fileName in fileNames)
            {
                using var form = new MultipartFormDataContent();
                var imagePath = Path.Combine(directoryPath, fileName);

                var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                form.Add(fileContent, "files", Path.GetFileName(imagePath));

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("http://localhost:4000/adm1h/location/create"),
                    Content = form,
                };

                using var response = client.SendAsync(request).ConfigureAwait(false).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
                var body = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();



                fileContent.Dispose();
                fileStream.Dispose();


                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {body}");
            }
        }
    }
}