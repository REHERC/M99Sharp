using M99Sharp.M99.Models;
using M99Sharp.M99.Systems.Cleaner;
using System;
using System.Collections.Generic;
using System.Linq;

namespace M99Sharp.M99.Systems.Parser
{
    public class Parser
    {
        public readonly CleanerToken[] source;

        public Parser(CleanerToken[] source_)
        {
            source = source_;
        }

        public ParserInstruction[] Parse()
        {
            if (source.Length == 0)
            {
                return new ParserInstruction[0];
            }

            List<ParserInstruction> result = new List<ParserInstruction>();

            for (int index = 0; index < source.Length; index++)
            {
                CleanerToken Expect(int offset, params CleanerType[] expectation) => ExpectToken(source, index + offset, expectation);
                object[] GetArguments(int offset, out int skipped) => this.GetArguments(source, index + offset, out skipped);

                CleanerToken address = Expect(0, CleanerType.NUMERAL);
                Expect(1, CleanerType.COLUMN);
                CleanerToken instruction = Expect(2, CleanerType.LITTERAL, CleanerType.NUMERAL);

                switch (instruction.key)
                {
                    case CleanerType.NUMERAL:
                        result.Add(new ParserInstruction(address.filePosition, InstructionCode.RAW, address.GetShort(), instruction.GetLong()));
                        index += 2;
                        continue;
                    case CleanerType.LITTERAL:
                        result.Add(new ParserInstruction(address.filePosition, ParserInstruction.GetCode(instruction.value), address.GetShort(), GetArguments(3, out int skipped)));
                        index += 2 + skipped;
                        continue;
                }
            }

            return result.ToArray();
        }

        object[] GetArguments(CleanerToken[] tokens, int startIndex, out int skipped)
        {
            List<object> result = new List<object>();

            skipped = 0;

            for (int index = startIndex; index < tokens.Length; index += 2)
            {
                CleanerToken current = GetToken(tokens, index);
                CleanerToken next = GetToken(tokens, index, 1);

                bool noArg = skipped == 0 && current.key == CleanerType.NUMERAL && next.key == CleanerType.COLUMN;
                bool hasArg = current.key == CleanerType.LITTERAL || current.key == CleanerType.NUMERAL;
                bool continueScan = next.key == CleanerType.COMMA;

                if (noArg)
                {
                    break;
                }
                else if (hasArg)
                {
                    result.Add(current.value);

                    if (continueScan)
                    {
                        skipped += 2;
                        continue;
                    }
                    else
                    {
                        skipped += 1;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            return result.ToArray();
        }

        public CleanerToken ExpectToken(CleanerToken[] array, int index, params CleanerType[] expectation)
        {
            CleanerToken token = GetToken(array, index);

            if (expectation.Contains(token.key))
            {
                return token;
            }
            else
            {
                throw new Exception("Syntax error. implement custom exception type later");
            }
        }

        public CleanerToken GetToken(CleanerToken[] array, int index, int offset = 0)
        {
            int finalIndex = index + offset;

            if (finalIndex < 0 || finalIndex >= array.Length)
            {
                return CleanerToken.Empty;
            }
            else
            {
                return array[finalIndex];
            }
        }
    }
}
