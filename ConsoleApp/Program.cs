
List<Task> tasks =
[
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