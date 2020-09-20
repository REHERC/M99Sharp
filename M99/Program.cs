using M99.Errors;
using System;
using System.IO;

namespace M99
{
    internal class Program
    {
        public const string FILE_NAME = "instructions.m99";

        public static M99Process Process;

        static int Main(/*string[] args*/)
        {
            Log.Info("M99 Micro-assembly processor");

            try
            {
                Process = M99Process.Load(FILE_NAME);
                Process.Run();
            }
            catch (FileNotFoundException fnfe)
            {
                Log.Error($"FILE NOT FOUND");
                Log.Error(new FileInfo(fnfe.Message).Name);
                return 1;
            }
            catch (M99SyntaxError se)
            {
                Log.Error($"SYNTAX ERROR");
                Log.Error(se.Message);
                //Log.Error($"{FILE_NAME}:{se.Token.position}");
                return 100;
            }
            catch (M99Error err)
            {
                Log.Error($"ERROR");
                Log.Error(err.Message);
                return 99;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return -1;
            }

            return 0;
        }

    }
}
