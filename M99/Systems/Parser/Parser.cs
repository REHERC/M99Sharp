using M99.Data;
using M99.Errors;
using M99.Systems.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace M99.Systems.Parser
{
    public class Parser
    {
        public readonly Token[] tokens;

        public Parser(Token[] t)
        {
            tokens = t;
        }

        public ParsingInstruction[] Parse()
        {
            if (tokens.Length == 0)
            {
                return new ParsingInstruction[0];
            }

            Token[] result;
            result = CleanComments(tokens);
            result = CleanNewLines(result);
            result = ProcessNumeralSign(result);

            var parsed = Parse(result);

            return parsed;
        }

        private Token[] CleanComments(Token[] array)
        {
            List<Token> result = new List<Token>();
            Token[] input = array.Where(x => x.type != TokenType.SPACE).ToArray();

            CommentType comment = CommentType.NONE;

            for (int x = 0; x < input.Length; x++)
            {
                if (x >= input.Length)
                {
                    break;
                }

                Token current = Get(input, x);
                Token next = Get(input, x, 1);

                switch (comment)
                {
                    case CommentType.NONE:
                        if (current.type == TokenType.SLASH)
                        {
                            if (next.type == TokenType.SLASH)
                            {
                                comment = CommentType.SIMPLE;
                            }
                            else if (next.type == TokenType.STAR)
                            {
                                comment = CommentType.MULTILINE;
                            }
                        }
                        break;
                    case CommentType.MULTILINE:
                        if (current.type == TokenType.STAR && next.type == TokenType.SLASH)
                        {
                            comment = CommentType.NONE;
                            x += 2;
                        }
                        break;
                    case CommentType.SIMPLE:
                        if (current.type == TokenType.NEW_LINE)
                        {
                            comment = CommentType.NONE;
                        }
                        break;
                }

                if (comment == CommentType.NONE)
                {
                    result.Add(input[x]);
                }
            }

            return result.ToArray();
        }

        private Token[] CleanNewLines(Token[] array)
        {
            return array.Where(x => x.type != TokenType.NEW_LINE).ToArray();
        }

        public Token[] ProcessNumeralSign(Token[] array)
        {
            List<Token> result = new List<Token>();
            Token current = Token.Empty;
            Token next = Token.Empty;

            for (int x = 0; x < array.Length; x++)
            {
                current = Get(array, x);
                next = Get(array, x, 1);

                switch (current.type)
                {
                    case TokenType.DASH:
                        if (next.type == TokenType.NUMERAL)
                        {
                            next.Prepend('-');
                            result.Add(next);
                            x += 1;
                            continue;
                        }
                        break;
                }

                result.Add(current);
            }

            return result.ToArray();
        }

        public ParsingInstruction[] Parse(Token[] array)
        {
            List<ParsingInstruction> instructions = new List<ParsingInstruction>();

            for (int x = 0; x < array.Length; x++)
            {
                Token Expect(int offset, params TokenType[] expectation)
                {
                    return ExpectToken(array, x + offset, expectation);
                }

                Token[] GetArguments(int index, out int skipped)
                {
                    List<Token> result = new List<Token>();

                    skipped = 0;

                    for (int y = index; y < array.Length; y += 2)
                    {
                        Token current = Get(array, y);
                        Token next = Get(array, y, 1);

                        bool noArg = skipped == 0 && current.type == TokenType.NUMERAL && next.type == TokenType.COLUMN;
                        bool hasArg = current.type == TokenType.LITTERAL || current.type == TokenType.NUMERAL;
                        bool continueScan = next.type == TokenType.COMMA;

                        if (noArg)
                        {
                            break;
                        }
                        else if (hasArg)
                        {
                            result.Add(current);

                            if (continueScan)
                            {
                                skipped += 2;
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

                        continue;
                    }

                    return result.ToArray();
                }

                int address = Expect(0, TokenType.NUMERAL).GetInteger();

                Expect(1, TokenType.COLUMN);

                var operand = Expect(2, TokenType.LITTERAL, TokenType.NUMERAL);

                if (operand.type == TokenType.NUMERAL)
                {
                    instructions.Add(new ParsingInstruction(address, OpCode.VAL, operand.GetInteger()));
                    x += 2;
                    continue;
                }
                else if (operand.type == TokenType.LITTERAL)
                {
                    instructions.Add(new ParsingInstruction(address, Instruction.GetCode(operand.GetString()), GetArguments(x+3, out int skip).Select(x => x.value).ToArray()));
                    x += 2 + skip;
                    continue;
                }
                else
                {
                    throw new M99SyntaxError(operand, TokenType.LITTERAL, TokenType.NUMERAL);
                }
            }

            return instructions.ToArray();
        }

        public Token ExpectToken(Token[] array, int index, params TokenType[] expectation)
        {
            Token token = Get(array, index);

            if (expectation.Contains(token.type))
            {
                return token;
            }
            else
            {
                throw new M99SyntaxError(token, expectation);
            }
        }

        public Token Get(Token[] array, int index, int offset = 0)
        {
            int finalIndex = index + offset;

            if (finalIndex < 0 || finalIndex >= array.Length)
            {
                return Token.Empty;
            }
            else
            {
                return array[finalIndex];
            }
        }

        public Token GetNext(Token[] array, int index, TokenType lookupType)
        {
            var enumerator = array.Skip(index).GetEnumerator();

            while (enumerator.MoveNext())
            {
                TokenType currentType = enumerator.Current.type;

                if (currentType == TokenType.NEW_LINE)
                {
                    break;
                }
                else if (currentType != lookupType)
                {
                    return enumerator.Current;
                }
            }

            return Token.Empty;
        }
    }
}
