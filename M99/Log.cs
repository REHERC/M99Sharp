using System;

namespace M99
{
    public static class Log
    {
        public static void Message(ConsoleColor color, string msg)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = currentColor;
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
    }
}
