using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public static class IO
{
    public static void Message(ConsoleColor color, string msg)
    {
        var currentColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(msg);
        Console.ForegroundColor = currentColor;
    }

    public static void Write(ConsoleColor color, string msg)
    {
        var currentColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(msg);
        Console.ForegroundColor = currentColor;
    }

    public static void ClearLine(int line)
    {
        void ResetPos()
        {
            Console.CursorTop = line;
            Console.CursorLeft = 0;
        }

        ResetPos();

        StringBuilder sb = new StringBuilder();
        sb.Append(' ', Console.BufferWidth);

        Console.Write(sb.ToString());

        ResetPos();
    }

    public static void SkipLine(uint count = 1)
    {
        for (uint x = 1; x <= count; x++)
        {
            Console.WriteLine();
        }
    }

    public static void Info(string msg)
    {
        Message(ConsoleColor.White, $"[INF] {msg}");
    }

    public static void Error(string msg)
    {
        Message(ConsoleColor.Red, $"[ERR] {msg}");
    }

    public static void Warning(string msg)
    {
        Message(ConsoleColor.Yellow, $"[WRN] {msg}");
    }

    public static long ReadNumber()
    {
        int pos_x;
        int pos_y;

        if (Console.CursorLeft > 0)
        {
            Console.WriteLine();
        }

        pos_x = Console.CursorLeft;
        pos_y = Console.CursorTop;

        while (true)
        {
            Console.CursorLeft = pos_x;
            Console.CursorTop = pos_y;

            ClearLine(pos_y);
            Write(ConsoleColor.Cyan, "[<<<] ");

            string input = Console.ReadLine();

            if (long.TryParse(input, out long number))
            {
                return number;
            }
        }
    }

    public static void PrintNumber(long number)
    {
        Write(ConsoleColor.Cyan, $"[>>>] ");
        Message(ConsoleColor.Gray, $"{number}");
    }
}
