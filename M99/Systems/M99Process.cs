using M99;
using M99.Systems.Classifier;
using M99.Systems.Parser;
using M99.Systems.Tokenizer;
using System;
using System.IO;
using System.Linq;

public class M99Process
{
    public readonly FileInfo file;

    public Classifier Classifier { get; private set; }

    public Tokenizer Tokenizer { get; private set; }

    public Parser Parser { get; private set; }

    public M99Process(FileInfo source)
    {
        file = source;

        if (!file.Exists)
        {
            throw new FileNotFoundException(source.FullName);
        }
    }

    public static M99Process Load(string fileName)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        return new M99Process(new FileInfo(path));
    }

    public void Run()
    {
        Classifier = new Classifier(file);
        CharToken[] chars = Classifier.Classify();

        Tokenizer = new Tokenizer(chars);
        Token[] tokens = Tokenizer.Tokenize();

        Parser = new Parser(tokens);
        ParsingInstruction[] instructions = Parser.Parse();

        PrintInstructions(instructions);
    }

    public void PrintChars(CharToken[] chars)
    {
        Log.Warning("Char classification:");
        foreach (var x in chars)
            Console.WriteLine($"{x.value}\t{x.position}\t\t{x.type}");
    }

    private void PrintTokens(Token[] tokens)
    {
        Log.Warning("Token list:");
        int pos = 0;
        foreach (var x in tokens)
        {
            int y = Console.CursorTop;

            Console.SetCursorPosition(0, y);
            Console.Write(pos);

            Console.SetCursorPosition(4, y);
            Console.Write(x.position);

            Console.SetCursorPosition(15, y);
            Console.Write(x.type);

            Console.SetCursorPosition(25, y);
            Console.Write(x.value);

            Console.WriteLine();

            pos++;
        }
    }

    public void PrintInstructions(ParsingInstruction[] instructions)
    {
        Log.Warning("Instruction list (parsed):");
        Console.WriteLine("ADDRESS\t\tINSTRUCTION\tARGUMENTS");
        foreach (var x in from i in instructions orderby i.address select i)
        {
            Console.WriteLine($"{x.address}\t\t{x.operand}\t\t{string.Join(", ", x.values)}");
        }
    }
}