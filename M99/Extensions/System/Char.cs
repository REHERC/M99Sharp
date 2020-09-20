using System.Linq;

public static class CharExtensions
{
    public static bool InRange(this char c, char min, char max)
    {
        return min <= c && c <= max;
    }

    public static bool OneOf(this char c, params char[] values)
    {
        return values.Contains(c);
    }
}