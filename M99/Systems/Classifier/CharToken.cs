using M99.Data;

namespace M99.Systems.Classifier
{
    public struct CharToken
    {
        public static CharToken Empty => new CharToken(FilePosition.Empty, CharType.NONE, '\0');
        public CharType type;
        public char value;
        public FilePosition position;

        public CharToken(FilePosition p, CharType t, char v)
        {
            position = p;
            type = t;
            value = v;
        }
    }
}
