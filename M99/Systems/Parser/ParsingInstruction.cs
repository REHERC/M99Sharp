using M99.Data;
using System.Collections.Generic;

namespace M99.Systems.Parser
{
    public struct ParsingInstruction
    {
        public static ParsingInstruction Empty => new ParsingInstruction(-1, OpCode.VAL, null);
        public OpCode operand;
        public int address;
        public object[] values;

        public ParsingInstruction(int a, OpCode c, params object[] v)
        {
            operand = c;
            address = a;
            values = v;
        }

        public static readonly Dictionary<OpCode, string> CompileOpCodes = new Dictionary<OpCode, string>()
        {
            { OpCode.STR, "0"           },
            { OpCode.LDA, "1"           },
            { OpCode.LDB, "2"           },
            { OpCode.MOV, "3"           },
            { OpCode.ADD, "400"         },
            { OpCode.SUB, "401"         },
            { OpCode.JMP, "5"           },
            { OpCode.JPP, "6"           },
            { OpCode.JEQ, "7"           },
            { OpCode.JNE, "8"           },
            { OpCode.VAL, string.Empty  },
            { OpCode.RAW, string.Empty  }
        };

        /*public static readonly Dictionary<OpCode, KeyValuePair<Type[], Func<string, object[]>>[]> InstructionSignatures = new Dictionary<OpCode, KeyValuePair<Type[], Func<string, object[]>>[]>()
        {
            {
                OpCode.STR,
                new KeyValuePair<Type, Func<string, object[]>>()
                {
                    new KeyValuePair<Type[], Func<string, object[]>>(new Type[] { typeof(int) }, )
                }
            }
        };*/
            /*{ 
                OpCode.STR, 
                new Type[][]
                {
                    new Type[] { typeof(int) }
                }
            },
            { 
                OpCode.LDA,
                new Type[][]
                {
                    new Type[] { typeof(int) }
                }
            },
            { 
                OpCode.LDB,
                new Type[][]
                {
                    new Type[] { typeof(int) }
                }
            },
            { 
                OpCode.MOV,
                new Type[][]
                {
                    new Type[] { typeof(int) },
                    new Type[] { typeof(int), typeof(int) },
                    new Type[] { typeof(string), typeof(int) },
                    new Type[] { typeof(int), typeof(string) },
                    new Type[] { typeof(string), typeof(string) }
                }
            },
            { 
                OpCode.ADD,
                new Type[0][] // ADD TAKES NO ARGUMENTS
            },
            { 
                OpCode.SUB,
                new Type[0][] // SUB TAKES NO ARGUMENTS 
            },
            { 
                OpCode.JMP, 
                new Type[][]
                {
                    new Type[] { typeof(int) }
                }
            },
            { 
                OpCode.JPP,
                new Type[][]
                {
                    new Type[] { typeof(int) }
                }
            },
            { 
                OpCode.JEQ,
                new Type[][]
                {
                    new Type[] { typeof(int) }
                }
            },
            { 
                OpCode.JNE,
                new Type[][]
                {
                    new Type[] { typeof(int) }
                }
            },
            { 
                OpCode.VAL,
                new Type[][]
                {
                    new Type[] { typeof(int) }
                }
            },
            { 
                OpCode.RAW,
                new Type[][]
                {
                    new Type[] { typeof(int) }
                }
            }
        };*/

        public int Compile()
        {
            string instruction = CompileOpCodes[operand];

            /*Type[][] signatures = InstructionSignatures[operand];

            foreach (Type[] signature in signatures)
            {
                if (signature.Length != values.Length)
                {
                    continue;
                }

                bool match = true;
                for (int index = 0; index < signature.Length; index++)
                {
                    match &= signature[index] == values[index].GetType();
                    if (!match)
                    {
                        break;
                    }
                }

                if (match)
                {

                }
            }
            */
            return 0;
        }
    }
}
