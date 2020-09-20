namespace M99Sharp.M99.Systems.Runner
{
    public static class Constants
    {
        public const short INPUT_OUTPUT = 99;

        public static readonly InstructionDelegate[] runnerMethods;

        static Constants()
        {
            runnerMethods = new InstructionDelegate[9]
            {
                STR,
                LDA,
                LDB,
                MOV,
                MATH,
                JMP,
                JPP,
                JEQ,
                JNE
            };
        }

        public static void STR(ref long[] program, ref MemoryRegisters registers, ref InstructionData instruction)
        {
            if (instruction.xy == INPUT_OUTPUT)
            {
                IO.PrintNumber(registers.r);
            }
            else 
            {
                program[instruction.xy] = registers.r;
            }

            ++registers.program_counter;
        }

        public static void LDA(ref long[] program, ref MemoryRegisters registers, ref InstructionData instruction)
        {
            if (instruction.xy == INPUT_OUTPUT)
            {
                registers.a = IO.ReadNumber();
            }
            else
            {
                registers.a = program[instruction.xy];
            }

            ++registers.program_counter;
        }

        public static void LDB(ref long[] program, ref MemoryRegisters registers, ref InstructionData instruction)
        {
            if (instruction.xy == INPUT_OUTPUT)
            {
                registers.b = IO.ReadNumber();
            }
            else
            {
                registers.b = program[instruction.xy];
            }

            ++registers.program_counter;
        }

        public static void MOV(ref long[] program, ref MemoryRegisters registers, ref InstructionData instruction)
        {
            long temp = 0;

            switch (instruction.x)
            {
                case 0: temp = registers.r; break;
                case 1: temp = registers.a; break;
                case 2: temp = registers.b; break;
                case 3: /*  throw error  */ break;
            }

            switch (instruction.y)
            {
                case 0: registers.r = temp; break;
                case 1: registers.a = temp; break;
                case 2: registers.b = temp; break;
                case 3: /*  throw error  */ break;
            }

            ++registers.program_counter;
        }
        
        public static void MATH(ref long[] program, ref MemoryRegisters registers, ref InstructionData instruction)
        {
            switch (instruction.xy)
            {
                case 0:
                    registers.r = registers.a + registers.b;
                    break;
                case 1:
                    registers.r = registers.a - registers.b;
                    break;
            }

            ++registers.program_counter;
        }

        public static void JMP(ref long[] program, ref MemoryRegisters registers, ref InstructionData instruction)
        {
            registers.program_counter = instruction.xy;
        }

        public static void JPP(ref long[] program, ref MemoryRegisters registers, ref InstructionData instruction)
        {
            if (registers.r > 0)
            {
                registers.program_counter = instruction.xy;
            }
            else
            {
                ++registers.program_counter;
            }
        }

        public static void JEQ(ref long[] program, ref MemoryRegisters registers, ref InstructionData instruction)
        {
            if (registers.r == instruction.xy)
            {
                registers.program_counter += 2;
            }
            else
            {
                ++registers.program_counter;
            }
        }
        
        public static void JNE(ref long[] program, ref MemoryRegisters registers, ref InstructionData instruction)
        {
            if (registers.r != instruction.xy)
            {
                registers.program_counter += 2;
            }
            else
            {
                ++registers.program_counter;
            }
        }
    }
}
