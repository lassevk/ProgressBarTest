using ConsoleApp1;

var current = new ConsoleLines(10);

var tasks = Enumerable.Range(0, current.Count).Select(idx => Walker(s =>
{
    current.Set(idx, s + $" - Task #{idx + 1}");
})).ToArray();

await Task.WhenAll(tasks);

async Task Walker(Action<string> onProgress)
{
    int index = 0;
    while (index < 1000)
    {
        index += Random.Shared.Next(100);
        if (index > 1000)
            index = 1000;

        onProgress(ProgressBar.Get(index, 1000));
        await Task.Delay(Random.Shared.Next(100));
    }

    onProgress(ProgressBar.Get(1000, 1000));
}