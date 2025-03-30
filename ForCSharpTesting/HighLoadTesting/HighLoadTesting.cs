using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ForCSharpTesting.HighLoadTesting;

public static class HighLoadTesting
{
    private const int Iterations = 250; //10;
    private const int TasksCount = 2;

    const int __30MIN_SECONDS = 30 * 60; // 30 min
    private static int RPSCount;
    private static List<int> RPSList;
    private static List<long> RequestsDurations;
    private static bool CheckRPS;

    private static bool _writeDebugInfo = true;

    public static async Task HighLoadFilesMultiThread()
    {
        if (_writeDebugInfo) Console.WriteLine($"Method {nameof(HighLoadFilesMultiThread)} start");
        RPSCount = 0;
        RPSList = new(__30MIN_SECONDS);
        RequestsDurations = new();
        CheckRPS = true;

        if (_writeDebugInfo) Console.WriteLine($"Thread with method {nameof(GetCurrentRPSSnapshot)} start");
        var RPSThread = new Thread(GetCurrentRPSSnapshot);
        RPSThread.Start();

        if (_writeDebugInfo) Console.WriteLine($"Start creating {TasksCount} tasks");
        var tasks = new List<Task>(TasksCount);
        for (int i = 0; i < TasksCount; i++)
        {
            if (_writeDebugInfo) Console.WriteLine($"Task {i} creating");
            var task = new Task(HighLoadFiles);
            tasks.Add(task);

            if (_writeDebugInfo) Console.WriteLine($"Task {i} start");
            task.Start();
        }

        if (_writeDebugInfo) Console.WriteLine($"Await tasks");
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
        client.Timeout = TimeSpan.FromMinutes(10);

        var stopwatch = new Stopwatch();

        for (int i = 0; i < Iterations; i++)
        {
            RPSCount++;
            if (_writeDebugInfo) Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: Start request");
            

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

            if (_writeDebugInfo) Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: End request");
            RPSCount--;

            if (_writeDebugInfo) Console.WriteLine($"Current RPS: {RPSCount}");
        }
    }
}