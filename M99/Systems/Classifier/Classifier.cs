using M99.Data;
using System.IO;

namespace M99.Systems.Classifier
{
    public class Classifier
    {
        public readonly FileInfo filePath;

        public readonly string rawContent;

        public Classifier(FileInfo source)
        {
            filePath = source;

            rawContent = File.ReadAllText(filePath.FullName);
        }

        public CharToken[] Classify()
        {
            string content = rawContent.ToLowerInvariant();

            CharToken[] tokens = new CharToken[content.Length];
            CharType ctCurr = CharType.NONE;
            CharType ctLast = CharType.NONE;

            int linePosition = 1;
            int charPosition = 1;

            for (int x = 0; x < tokens.Length; x++)
            {
                char c = content[x];
                
                if (c.InRange('a', 'z'))
                {
                    ctCurr = CharType.LITTERAL;
                }
                else if (c.InRange('0', '9'))
                {
                    ctCurr = CharType.NUMERAL;
                }
                else if (c.OneOf(' ', '\t'))
                {
                    ctCurr = CharType.SPACE;
                }
                else if (c.Equals('-'))
                {
                    ctCurr = CharType.DASH;
                }
                else if (c.Equals('+'))
                {
                    ctCurr = CharType.PLUS;
                }
                else if (c.Equals('/'))
                {
                    ctCurr = CharType.SLASH;
                }
                else if (c.Equals('*'))
                {
                    ctCurr = CharType.STAR;
                }
                else if (c.Equals(','))
                {
                    ctCurr = CharType.COMMA;
                }
                else if (c.Equals(':'))
                {
                    ctCurr = CharType.COLUMN;
                }
                else if (c.OneOf('\n', '\r'))
                {
                    ctCurr = CharType.NEW_LINE;

                    if (ctCurr != ctLast) 
                    {
                        linePosition++;
                        charPosition = 1;
                    }
                }
                else
                {
                    ctCurr = CharType.OTHER;
                }

                tokens[x] = new CharToken(new FilePosition(linePosition, charPosition), ctCurr, rawContent[x]);

                ctLast = ctCurr;
                charPosition++;
            }

            for (int x = 0; x < tokens.Length; x++)
            {
                CharToken token = tokens[x];

                if (token.type == CharType.NEW_LINE)
                {
                    tokens[x].position = new FilePosition(token.position.line - 1, token.position.offset);
                }
            }

            return tokens;
        }
    }
}
