using System.Dynamic;
using System.Globalization;

namespace ConsoleApp1;

public class ProgressBar
{
    private static readonly char[] Blocks = [
        '\u258f',
        '\u258e',
        '\u258d',
        '\u258c',
        '\u258b',
        '\u258a',
        '\u2589',
        '\u2588'
    ];

    public static string Get(int current, int total)
    {
        var percent = (current * 100.0M / total);
        var blockCount = percent / 4.0M;
        var whole = (int)Math.Floor(blockCount);
        var fraction = (int)((blockCount - Math.Floor(blockCount)) * 8);

        const int length = 25;
        Span<char> chars = stackalloc char[length];
        for (int index = 0; index < whole; index++)
            chars[index] = '\u2588';
        if (fraction != 0)
        {
            chars[whole] = Blocks[fraction];
            for (int index = whole + 1; index < length; index++)
                chars[index] = ' ';
        }
        else
        {
            for (int index = whole; index < length; index++)
                chars[index] = ' ';
        }

        return $"[{chars}] {percent,5:0.0}%";
    }
}