using M99Sharp.M99;
using System;

namespace M99Sharp
{
    class Program
    {
        public const string FILE_NAME = "instructions.m99";

        public static M99Program M99 { get; private set; }

        static void Main()
        {
            IO.Message(ConsoleColor.White, "*** M99 Micro-assembly processor ***");
            IO.Message(ConsoleColor.Gray, "M99 was originally created by");
            IO.Message(ConsoleColor.Gray, "Martin QUINSON and Philippe MARQUET");
            IO.Message(ConsoleColor.Gray, "as part of the InfoSansOrdi project.");
            IO.SkipLine();
            IO.Message(ConsoleColor.Gray, "Read more at:");
            IO.Message(ConsoleColor.Gray, "github.com/InfoSansOrdi/M999#readme");
            IO.SkipLine(2);

            try
            {
                M99 = M99Program.Load(FILE_NAME);
                M99.CompileAndExecute();
            }
            catch (Exception e)
            {
                IO.Error(e.ToString());
            }
        }
    }
}
