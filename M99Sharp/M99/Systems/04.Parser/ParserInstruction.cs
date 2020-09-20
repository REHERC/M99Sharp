using M99Sharp.M99.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static EnumExtensions;

namespace M99Sharp.M99.Systems.Parser
{
    public class ParserInstruction : GenericToken<InstructionCode, object[]>
    {
        public static ParserInstruction Empty => new ParserInstruction(FilePosition.Empty, InstructionCode.RAW, 0);

        public readonly short address;

        public ParserInstruction(FilePosition fp, InstructionCode pt, short a, params object[] o) : base(fp, pt, o)
        {
            address = a;
        }

        public static InstructionCode GetCode(string value)
        {
            return GetEnum<InstructionCode>(value);
        }

        public Type[] Signature()
        {
            return Arguments().Select(arg => arg.GetType()).ToArray();
        }

        public object[] Arguments()
        {
            List<object> result = new List<object>();

            foreach (object item in value)
            {
                if (item is string litteralLong && long.TryParse(litteralLong, out long numberLong))
                {
                    result.Add(numberLong);
                }
                else if (item is string litteralInt && int.TryParse(litteralInt, out int numberInt))
                {
                    result.Add(numberInt);
                }
                else
                {
                    result.Add(item);
                }
            }

            return result.ToArray();
        }
    }
}