using M99Sharp.M99.Systems.Classifier;
using M99Sharp.M99.Systems.Cleaner;
using M99Sharp.M99.Systems.Parser;
using M99Sharp.M99.Systems.Tokenizer;
using System;
using System.Text;

public static class Debug
{
    public static void Print(this ClassifierToken[] tokens)
    {
        IO.Warning("Classifier tokens:");
        foreach (ClassifierToken token in tokens)
        {
            IO.Message(ConsoleColor.Gray, $"{token.filePosition}\t{token.value}\t{token.key}");
        }
    }

    public static void Print(this TokenizerToken[] tokens)
    {
        IO.Warning("Tokenizer tokens:");
        int index = 0;
        foreach (TokenizerToken token in tokens)
        {
            IO.Message(ConsoleColor.Gray, $"{index}\t{token.filePosition}\t{token.value}\t{token.key}");
            index++;
        }
    }

    public static void Print(this CleanerToken[] tokens)
    {
        IO.Warning("Cleaned tokens:");
        int index = 0;
        foreach (CleanerToken token in tokens)
        {
            IO.Message(ConsoleColor.Gray, $"{index}\t{token.filePosition}\t{token.value}\t{token.key}");
            index++;
        }
    }

    public static void Print(this ParserInstruction[] tokens)
    {
        IO.Warning("Parsed instructions:");
        foreach (ParserInstruction token in tokens)
        {
            IO.Message(ConsoleColor.Gray, $"{token.address.ToString("00")}:{token.key.ToString().ToLower()} {string.Join(", ", token.value)}");
        }
    }

    public static void Print(this long[] tokens)
    {
        IO.Warning("Builder memory:");

        StringBuilder formatBuilder = new StringBuilder();

        formatBuilder.Append('0', (tokens.Length - 1).ToString().Length);
        string addressFormat = formatBuilder.ToString();

        formatBuilder = new StringBuilder();
        formatBuilder.Append('0', long.MaxValue.ToString().Length);
        string valueFormat = formatBuilder.ToString();

        for (int index = 0; index < tokens.Length; index++)
        {
            IO.Message(ConsoleColor.Gray, $"{index.ToString(addressFormat)}:{tokens[index].ToString(valueFormat)}");
        }
    }
}