using M99.Data;
using M99.Errors;
using M99.Systems.Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace M99.Systems.Compiler
{
    public class Compiler
    {
        public readonly ParsingInstruction[] instructions;

        public Compiler(ParsingInstruction[] i)
        {
            instructions = i;
        }

        public int[] Compile()
        {
            int[] storage = new int[99];

            for (int address = 0; address < storage.Length; address++)
            {
                storage[address] = 0;
            }

            HashSet<int> allocated = new HashSet<int>();
            foreach (ParsingInstruction instruction in instructions)
            {
                if (!allocated.Contains(instruction.address))
                {
                    storage[instruction.address] = instruction.Compile();
                    allocated.Add(instruction.address);
                }
                else
                {
                    throw new M99AllocationError(instruction.address, "this memory address is already allocated and can't be set twice during compilation");
                }
            }

            return storage;
        }
    }
}
