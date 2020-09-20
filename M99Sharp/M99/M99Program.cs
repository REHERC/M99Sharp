using M99Sharp.M99.Systems.Builder;
using M99Sharp.M99.Systems.Classifier;
using M99Sharp.M99.Systems.Cleaner;
using M99Sharp.M99.Systems.Parser;
using M99Sharp.M99.Systems.Runner;
using M99Sharp.M99.Systems.Tokenizer;
using System;
using System.IO;

namespace M99Sharp.M99
{
    public class M99Program
    {
        public readonly FileInfo file;

        public Classifier Classifier { get; private set; }

        public Tokenizer Tokenizer { get; private set; }

        public Cleaner Cleaner { get; private set; }

        public Parser Parser { get; private set; }

        public Builder Builder { get; private set; }

        public Runner Runner { get; private set; }


        private long[] Program = null;

        public M99Program(FileInfo source)
        {
            file = source;

            if (!file.Exists)
            {
                throw new FileNotFoundException(source.FullName);
            }
        }

        public static M99Program Load(string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            return new M99Program(new FileInfo(path));
        }

        public void CompileAndExecute()
        {
            Compile();
            Execute();
        }

        public void Compile()
        {
            Classifier = new Classifier(file);
            ClassifierToken[] classifierResult = Classifier.Classify();
            //classifierResult.Print();

            Tokenizer = new Tokenizer(classifierResult);
            TokenizerToken[] tokenizerResult = Tokenizer.Tokenize();
            //tokenizerResult.Print();

            Cleaner = new Cleaner(tokenizerResult);
            CleanerToken[] cleanerResult = Cleaner.Clean();
            //cleanerResult.Print();

            Parser = new Parser(cleanerResult);
            ParserInstruction[] parserResult = Parser.Parse();
            //parserResult.Print();

            Builder = new Builder(parserResult);
            long[] builderResult = Builder.Build();
            //builderResult.Print();

            Program = builderResult;
        }

        public void Execute()
        {
            if (Program != null)
            {
                Runner = new Runner(Program);
                Runner.Run();
            }
        }
    }
}
