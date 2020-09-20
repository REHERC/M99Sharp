using M99Sharp.M99.Models;
using System.IO;

namespace M99Sharp.M99.Systems.Classifier
{
    public class Classifier
    {
        public readonly FileInfo filePath;
        public readonly string text;

        public Classifier(FileInfo source)
        {
            filePath = source;
            text = File.ReadAllText(filePath.FullName);
        }

        public ClassifierToken[] Classify()
        {
            string content = text.ToLowerInvariant();

            if (content.Length == 0)
            {
                return new ClassifierToken[0];
            }

            ClassifierToken[] result = new ClassifierToken[content.Length];

            ClassifierType ctCurr = ClassifierType.NONE;
            ClassifierType ctLast = ClassifierType.NONE;

            int linePosition = 1;

            for (int index = 0; index < content.Length; index++)
            {
                char c = content[index];

                if (c.InRange('a', 'z'))
                {
                    ctCurr = ClassifierType.LITTERAL;
                }
                else if (c.InRange('0', '9'))
                {
                    ctCurr = ClassifierType.NUMERAL;
                }
                else if (c.OneOf(' ', '\t'))
                {
                    ctCurr = ClassifierType.SPACE;
                }
                else if (c.Equals('-'))
                {
                    ctCurr = ClassifierType.DASH;
                }
                else if (c.Equals('+'))
                {
                    ctCurr = ClassifierType.PLUS;
                }
                else if (c.Equals('/'))
                {
                    ctCurr = ClassifierType.SLASH;
                }
                else if (c.Equals('*'))
                {
                    ctCurr = ClassifierType.STAR;
                }
                else if (c.Equals(','))
                {
                    ctCurr = ClassifierType.COMMA;
                }
                else if (c.Equals(':'))
                {
                    ctCurr = ClassifierType.COLUMN;
                }
                else if (c.OneOf('\n', '\r'))
                {
                    ctCurr = ClassifierType.NEW_LINE;

                    if (ctCurr != ctLast)
                    {
                        linePosition++;
                    }
                }
                else
                {
                    ctCurr = ClassifierType.NONE;
                }

                result[index] = new ClassifierToken(new FilePosition(linePosition), ctCurr, content[index]);

                ctLast = ctCurr;
            }

            return result;
        }
    }
}
