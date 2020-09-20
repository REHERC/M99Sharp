using M99.Data;
using M99.Errors;

namespace M99.Systems.Tokenizer
{
    public struct Token
    {
        public static Token Empty => new Token(FilePosition.Empty, TokenType.NONE, string.Empty);
        public TokenType type;
        public string value;
        public FilePosition position;

        public Token(FilePosition p, TokenType t, string v)
        {
            type = t;
            value = v;
            position = p;
        }

        public void Append(string content)
        {
            value = $"{value}{content}";
        }

        public void Append(char content)
        {
            value = $"{value}{content}";
        }

        public void Prepend(string content)
        {
            value = $"{content}{value}";
        }

        public void Prepend(char content)
        {
            value = $"{content}{value}";
        }

        public string GetString()
        {
            return value;
        }

        public int GetInteger()
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            else
            {
                throw new M99Error($"An error occured while trying to parse \"{value}\" as an integer");
            }
        }
    }
}
