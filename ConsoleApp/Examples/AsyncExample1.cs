
namespace ConsoleApp.Examples;

public static class AsyncExample
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
        // await task.WaitAsync(TimeSpan.FromSeconds(5));

        Console.WriteLine($"Step 2, Thread Id: {Thread.CurrentThread.ManagedThreadId}");

        await Task.WhenAll(tasks);

        Console.WriteLine($"Step 3, Thread Id: {Thread.CurrentThread.ManagedThreadId}");

        static async Task RunEachStep()
        {
            Console.WriteLine($"Step 4, Thread Id: {Thread.CurrentThread.ManagedThreadId}");

            HttpClient client = new();
            await client.GetAsync("https://www.google.com");

            Console.WriteLine($"Step 5, Thread Id: {Thread.CurrentThread.ManagedThreadId}");
        }
    }

    public static async Task RunSyncToAsyncMethod()
    {
        string name = await GetNameAsync();
        Console.WriteLine($"Name: {name}, Thread Id: {Thread.CurrentThread.ManagedThreadId}");

        Task<string> GetNameAsync()
        {
            return Task.FromResult(GetName());
        }

        string GetName()
        {
            return "Hello";
        }
    }
    
    public static async Task RunTaskRunWithThreadPoolBehavior()
    {
        HttpClient client = new();
        int count = 10;
        string url = "https://httpstat.us/200?sleep=2000";
        List<Task> tasks = new();

        for (int i = 0; i < count; i++)
        {
            var index = string.Format("{0:D2}", i);
            tasks.Add(Task.Run(async () =>
            {
                HttpClient client = new();

                Console.WriteLine($"Part 1, {index} Send Request >>, Thread Id: {Thread.CurrentThread.ManagedThreadId}");

                string result = await client.GetStringAsync(url);

                Console.WriteLine($"Part 1, {index} Get Result <<, Thread Id: {Thread.CurrentThread.ManagedThreadId}");
            }));

            tasks.Add(Task.Run(async () =>
            {
                HttpClient client = new();
                
                Console.WriteLine($"Part 2, {index} Send Request >>, Thread Id: {Thread.CurrentThread.ManagedThreadId}");

                string result = await client.GetStringAsync(url);

                Console.WriteLine($"Part 2, {index} Get Result <<, Thread Id: {Thread.CurrentThread.ManagedThreadId}");
            }));
        }

        await Task.WhenAll(tasks);
    }

    public static async Task RunTaskStatus()
    {
        List<Task> tasks = 
        [
            RunAsync(),
            RunAsync(),
            RunAsync(),
            RunAsync(),
        ];

        try
        {
            await Task.WhenAll(tasks);
        }
        catch(Exception ex)
        {
        }

        foreach (var task in tasks)
        {
            Console.WriteLine($"Task Id: {task.Id}, Status: {task.Status}, Thread Id: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"IsCompleted: {task.IsCompleted}");
            Console.WriteLine($"IsFaulted: {task.IsFaulted}");
            Console.WriteLine($"IsCanceled: {task.IsCanceled}");
        }

        static Task RunAsync()
        {
            Random random = new();
            if (random.Next(0, 2) == 0)
            {
                return Task.FromCanceled(new CancellationToken(true));
            }

            return Task.Run(() =>
            {
                Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(2000);
            });
        }
    }
}


