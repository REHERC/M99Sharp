using System;
using System.Linq;
using System.Text;

namespace M99Sharp.M99.Systems.Runner
{
    public class Runner
    {
        public readonly long[] source;

        public Runner(long[] source_)
        {
            source = source_;
        }

        public void Run(short entryPoint = 0)
        {
            long[] program = source.Clone() as long[];

            MemoryRegisters memory = new MemoryRegisters(entryPoint);

            while (memory.program_counter != 99)
            {
                memory.instruction_raw = program[memory.program_counter];

                RunInstruction(ref program, ref memory);
            }
        }

        private void RunInstruction(ref long[] program, ref MemoryRegisters memory)
        {
            long instr = memory.instruction_raw;

            if (instr >= 0 && instr < 900)
            {
                InstructionData data = new InstructionData(instr);

                Constants.runnerMethods[data.operand](ref program, ref memory, ref data);
            }
            else
            {

            }
        }

        
    }
}
