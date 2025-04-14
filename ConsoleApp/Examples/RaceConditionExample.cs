namespace ConsoleApp.Examples;
public static class RaceConditionExample
{
    private static object _lock = new object();
    public static int Counter { get; set; } = 0;

    public static async Task RunCounterIncrement()
    {
        Task t1 = Task.Run(() => IncrementCounter());
        Task t2 = Task.Run(() => IncrementCounter());
        await Task.WhenAll(t1, t2);
        Console.WriteLine($"Counter: {Counter}");
    }

    private static void IncrementCounter()
    {
        for (int i = 0; i < 1000000; i++)
        {
            lock (_lock)
            {
                Counter++;
            }
        }
    }
}