namespace M99Sharp.M99.Systems.Runner
{
    public struct MemoryRegisters
    {
        public short program_counter;
        public long instruction_raw;
        public long a;
        public long b;
        public long r;

        public MemoryRegisters(short entry = 0)
        {
            program_counter = entry;
            instruction_raw = 0;
            a = 0;
            b = 0;
            r = 0;
        }
    }
}
