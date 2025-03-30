using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace ForCSharpTesting.HighLoadTesting;

public static class HighLoadTesting
{
    private const int Iterations = 2; //10;
    private const int TasksCount = 10; //200;

    const int __30MIN_SECONDS = 30 * 60; // 30 min
    private static int RPSCount;
    private static List<int> RPSList;
    private static List<long> RequestsDurations;
    private static bool CheckRPS;

    public static async Task HighLoadFilesMultiThread()
    {
        RPSCount = 0;
        RPSList = new(__30MIN_SECONDS);
        RequestsDurations = new();
        CheckRPS = true;

        var RPSThread = new Thread(GetCurrentRPSSnapshot);
        RPSThread.Start();


        var tasks = new List<Task>(TasksCount);
        for (int i = 0; i < TasksCount; i++)
        {
            var task = new Task(HighLoadFiles);
            tasks.Add(task);

            task.Start();
        }

        await Task.WhenAll(tasks);

        CheckRPS = false;

        Console.WriteLine(GetHighLoadFilesMultiThreadResult());
    }

    private static string GetHighLoadFilesMultiThreadResult()
    {
        var result = new StringBuilder();

        result.AppendLine();
        result.AppendLine("====================================================================");
        result.AppendLine($"~RPS: {RPSList.Average()}");
        result.AppendLine($"~RequestDuration: {RequestsDurations.Average() / 1000}s");

        return result.ToString();
    }

    private static void GetCurrentRPSSnapshot()
    {
        while (CheckRPS)
        {
            RPSList.Add(RPSCount);

            Thread.Sleep(1000);
        }
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

        using var client = new HttpClient();
        var stopwatch = new Stopwatch();

        for (int i = 0; i < Iterations; i++)
        {
            RPSCount++;
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: Start request");
            

            using var form = new MultipartFormDataContent();
            
            foreach (var fileName in fileNames)
            {
                var imagePath = Path.Combine(directoryPath, fileName);
                var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                form.Add(fileContent, "files", Path.GetFileName(imagePath));
            }

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://localhost:4000/adm1h/location/create"),
                Content = form,
            };

            stopwatch.Start();

            using var response = client.SendAsync(request).ConfigureAwait(false).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            var body = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            stopwatch.Stop();
            RequestsDurations.Add(stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {body}");
            RPSCount--;
        }
    }
}