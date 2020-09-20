namespace M99.Data
{
    public struct FilePosition
    {
        public static FilePosition Empty => new FilePosition(-1, -1);
        public long line;
        public long offset;

        public FilePosition(long line_, long offset_)
        {
            line = line_;
            offset = offset_;
        }

        public override string ToString()
        {
            return $"L{line},C{offset}";
        }

        public string Debug()
        {
            return $"line {line}:{offset}";
        }
    }
}
