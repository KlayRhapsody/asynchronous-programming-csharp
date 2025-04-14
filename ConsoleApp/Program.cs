
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
