namespace M99Sharp.M99.Models
{
    public struct FilePosition
    {
        public static FilePosition Empty => new FilePosition(1);
        public readonly int line;

        public FilePosition(int line_)
        {
            line = line_;
        }

        public override string ToString()
        {
            return $"L{line}";
        }
    }
}
