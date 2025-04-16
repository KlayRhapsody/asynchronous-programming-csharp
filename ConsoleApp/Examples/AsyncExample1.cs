
namespace ConsoleApp.Examples;

public static class AsyncExample1
{
    public static async Task RunConcurrentTasksWhenTaskDelay()
    {
        List<Task> tasks = new()
        {
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
        };

        await Task.WhenAll(tasks);

        static async Task RunAsync()
        {
            Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(2000);
        }
    }

    public static async Task RunConcurrentTasksWhenThreadSleep()
    {
        List<Task> tasks =
        [
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
        ];

        await Task.WhenAll(tasks);

        static async Task RunAsync()
        {
            Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(2000);
        }
    }

    public static async Task RunConcurrentTasksByTaskRun()
    {
        List<Task> tasks =
        [
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
        ];

        await Task.WhenAll(tasks);

        static Task RunAsync()
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(2000);
            });
        }
    }

    public static async Task RunAsyncStepByStep()
    {
        Console.WriteLine($"Step 1, Thread Id: {Thread.CurrentThread.ManagedThreadId}");

        List<Task> tasks = [];

        for (int i = 0; i < 10; i++)
        {
            tasks.Add(Task.Run(() => Thread.Sleep(10000)));
        }
        
        Task task = RunEachStep();
        tasks.Add(task);

        Console.WriteLine($"Step 2, Thread Id: {Thread.CurrentThread.ManagedThreadId}");

        Task.WhenAll(tasks).Wait();

        Console.WriteLine($"Step 3, Thread Id: {Thread.CurrentThread.ManagedThreadId}");

        static async Task RunEachStep()
        {
            Console.WriteLine($"Step 4, Thread Id: {Thread.CurrentThread.ManagedThreadId}");

            // await Task.Delay(2000);
            HttpClient client = new();
            await client.GetAsync("https://www.google.com");

            Console.WriteLine($"Step 5, Thread Id: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}


