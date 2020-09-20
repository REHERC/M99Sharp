using M99Sharp.M99.Systems.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace M99Sharp.M99.Systems.Builder
{
    public class Builder
    {
        public readonly ParserInstruction[] source;

        public Builder(ParserInstruction[] source_)
        {
            source = source_;
        }

        public long[] Build()
        {
            long[] result = new long[99];

            for (long init = 0; init < result.Length; init++)
            {
                result[init] = 0;
            }

            if (source.Length == 0)
            {
                result[0] = 599;
            }
            else
            {
                HashSet<short> initialisedAddresses = new HashSet<short>();

                foreach (ParserInstruction instruction in source)
                {
                    if (!initialisedAddresses.Contains(instruction.address))
                    {
                        result[instruction.address] = GetInstructionBytecode(instruction);

                        initialisedAddresses.Add(instruction.address);
                    }
                    else
                    {
                        // throw code address already initialised exception or something
                    }
                }
            }

            return result;
        }

        private long GetInstructionBytecode(ParserInstruction instruction)
        {
            MethodInfo method = GetInstructionBuilder(instruction);

            if (method != null)
            {
                long bytecode = (long)method.Invoke(this, instruction.Arguments());

                return bytecode;
            }
            else
            {
                // bruh throw an error
            }

            return -1;
        }

        private MethodInfo GetInstructionBuilder(ParserInstruction instruction)
        {
            const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

            Type type = typeof(Builder);
            MethodInfo[] methods = type.GetMethods(flags);

            foreach (MethodInfo method in methods)
            {
                if (Match(method, typeof(long), instruction))
                {
                    return method;
                }
            }

            return null;
        }

        private bool Match(MethodInfo method, Type returnType, ParserInstruction instruction)
        {
            string name = $"INSTR_{instruction.key}";
            bool nameMatch = method.Name.Equals(name);
            bool typeMatch = method.ReturnType.Equals(returnType) || returnType.IsSubclassOf(method.ReturnType);

            if (!(nameMatch && typeMatch))
            {
                return false;
            }

            Type[] methodSignature = method.GetParameters().Select(param => param.ParameterType).ToArray();
            Type[] instructionSignature = instruction.Signature();

            bool signatureMatch = methodSignature.Length == instructionSignature.Length;

            if (signatureMatch)
            {
                for (int index = 0; index < methodSignature.Length; index++)
                {
                    Type methodArgumentType = methodSignature[index];
                    Type instructionArgumentType = instructionSignature[index];
                    signatureMatch &= methodArgumentType.Equals(instructionArgumentType) || instructionArgumentType.IsSubclassOf(methodArgumentType);

                    if (!signatureMatch)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        #region Instruction Builders
        #region Constants
        #region Instructions
        protected const int RAW = 000;
        protected const int STR = 000;
        protected const int LDA = 100;
        protected const int LDB = 200;
        protected const int MOV = 300;
        protected const int ADD = 400;
        protected const int SUB = 401;
        protected const int JMP = 500;
        protected const int JPP = 600;
        protected const int JEQ = 700;
        protected const int JNE = 800;
        #endregion
        #region Registers
        protected const int REG_R = 0;
        protected const char REG_R_NAME = 'r';
        protected const int REG_A = 1;
        protected const char REG_A_NAME = 'a';
        protected const int REG_B = 2;
        protected const char REG_B_NAME = 'b';
        protected const int REG_ERR = 3;

        #endregion
        #endregion

        #region Utils
        protected long REGISTER(string name)
        {
            if (name.Length == 1)
            {
                switch (name.ToLower()[0])
                {
                    case REG_A_NAME: return REG_A;
                    case REG_B_NAME: return REG_B;
                    case REG_R_NAME: return REG_R;
                }
            }
            return REG_ERR;
        }
        #endregion

        protected long INSTR_RAW(long val)
        {
            return RAW + val;
        }

        protected long INSTR_STR(long addr)
        {
            return STR + addr;
        }

        protected long INSTR_LDA(long addr)
        {
            return LDA + addr;
        }

        protected long INSTR_LDB(long addr)
        {
            return LDB + addr;
        }

        protected long INSTR_MOV(long val)
        {
            return MOV + val;
        }

        protected long INSTR_MOV(long a, long b)
        {
            return INSTR_MOV(10 * a + b);
        }

        protected long INSTR_MOV(string a, string b)
        {
            return INSTR_MOV(REGISTER(a), REGISTER(b));
        }

        protected long INSTR_MOV(long a, string b)
        {
            return INSTR_MOV(a, REGISTER(b));
        }

        protected long INSTR_MOV(string a, long b)
        {
            return INSTR_MOV(REGISTER(a), b);
        }

        protected long INSTR_ADD()
        {
            return ADD;
        }

        protected long INSTR_SUB()
        {
            return SUB;
        }

        protected long INSTR_JMP(long addr)
        {
            return JMP + addr;
        }

        protected long INSTR_JPP(long addr)
        {
            return JPP + addr;
        }

        protected long INSTR_JEQ(long addr)
        {
            return JEQ + addr;
        }

        protected long INSTR_JNE(long addr)
        {
            return JNE + addr;
        }

        #endregion
    }
}
