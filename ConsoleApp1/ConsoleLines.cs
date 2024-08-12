namespace ConsoleApp1;

public class ConsoleLines
{
    private readonly ConsoleLine[] _lines;
    private readonly object _lock = new();
    
    public ConsoleLines(int count)
    {
        _lines = Enumerable.Range(0, count).Select(_ => new ConsoleLine()).ToArray();
        foreach (var line in _lines[..^1])
            Console.WriteLine();
    }

    public int Count => _lines.Length;

    private void GoToLine(int current, int next)
    {
        if (current == next)
            return;

        Console.Write(current > next ? $"\u001b[{current - next}A" : $"\u001b[{next - current}B");
        Console.Write($"\u001b[{_lines[next].Current.Length + 1}G");
    }

    public void Set(int index, string value)
    {
        lock (_lock)
        {
            if (_lines[index].Current == value)
                return;

            GoToLine(_lines.Length - 1, index);
            _lines[index].Set(value);
            GoToLine(index, _lines.Length - 1);
        }
    }

    public void Clear()
    {
        lock (_lock)
        {
            GoToLine(_lines.Length - 1, 0);
            int current = 0;
            for (int index = 0; index < _lines.Length; index++)
            {
                GoToLine(current, index);
                current = index;
                _lines[index].Clear();
            }
        }
    }
}