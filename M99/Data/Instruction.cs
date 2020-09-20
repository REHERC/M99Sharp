using static EnumExtensions;

namespace M99.Data
{
    public struct Instruction
    {
        public OpCode code;
        public object operand;
        public FilePosition position;

        public static OpCode GetCode(string value)
        {
            return GetEnum<OpCode>(value);
        }
    }
}
