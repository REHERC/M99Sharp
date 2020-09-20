using System;
using System.Linq;
using System.Text;

namespace M99Sharp.M99.Systems.Runner
{
    public class InstructionData
    {
        public readonly short operand;
        public readonly short x;
        public readonly short y;
        public readonly short xy;

        public InstructionData(long value)
        {
            operand = GetDigit(value, 3);
            x = GetDigit(value, 2);
            y = GetDigit(value, 1);
            xy = (short)(10 * x + y);
        }

        private short GetDigit(long value, short position)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('0', long.MaxValue.ToString().Length);

            string number = value.ToString(sb.ToString());
            char[] digits = number.Reverse().ToArray();

            try
            {
                return short.Parse(digits[position - 1].ToString());
            }
            catch
            {
                return 0;
            }
        }
    }
}
