using M99.Systems.Classifier;
using M99.Data;
using System.Collections.Generic;

namespace M99.Systems.Tokenizer
{
    public class Tokenizer
    {
        public readonly CharToken[] tokens;

        public readonly Dictionary<CharType, TokenType> conversionTable = new Dictionary<CharType, TokenType>()
        {
            { CharType.NONE, TokenType.NONE },
            { CharType.SPACE, TokenType.SPACE },
            { CharType.COLUMN, TokenType.COLUMN },
            { CharType.COMMA, TokenType.COMMA },
            { CharType.NUMERAL, TokenType.NUMERAL },
            { CharType.LITTERAL, TokenType.LITTERAL },
            { CharType.PLUS, TokenType.PLUS },
            { CharType.DASH, TokenType.DASH },
            { CharType.SLASH, TokenType.SLASH },
            { CharType.STAR, TokenType.STAR },
            { CharType.NEW_LINE, TokenType.NEW_LINE },
            //{ CharType.OTHER, TokenType.IGNORE },
        };

        public Tokenizer(CharToken[] t)
        {
            tokens = t;
        }

        public Token[] Tokenize()
        {
            if (tokens.Length == 0)
            {
                return new Token[0];
            }

            CharType ctLast = CharType.NONE;
            CharType ctCurr = CharType.NONE;
            bool forceCut = false;

            List<Token> tokenList = new List<Token>();
            Token currentToken = Token.Empty;

            FilePosition position = tokens[0].position;

            string buffer = string.Empty;

            for (int x = 0; x <= tokens.Length; x++)
            {
                CharToken ct = x == tokens.Length ? CharToken.Empty : tokens[x];

                ctCurr = ct.type;

                if (x > 0 && (ctCurr != ctLast || forceCut))
                {
                    if (conversionTable.TryGetValue(ctLast, out TokenType tt))
                    {
                        currentToken = new Token(position, tt, buffer);
                    }
                    else
                    {
                        currentToken = new Token(position, TokenType.NONE, buffer);
                    }

                    tokenList.Add(currentToken);

                    buffer = ct.value.ToString();
                    position = ct.position;
                }
                else
                {
                    buffer += ct.value;
                }

                switch (ctCurr)
                {
                    case CharType.NUMERAL:
                    case CharType.LITTERAL:
                    case CharType.NEW_LINE:
                        forceCut = false;
                        break;
                    default:
                        forceCut = true;
                        break;
                }

                ctLast = ctCurr;
            }

            return tokenList.ToArray();
        }
    }
}
