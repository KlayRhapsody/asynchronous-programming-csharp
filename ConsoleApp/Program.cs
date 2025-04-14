
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
    await Task.Delay(2000);
}