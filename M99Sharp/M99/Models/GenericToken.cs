using System;

namespace M99Sharp.M99.Models
{
    public abstract class GenericToken<ENUM, TYPE> where ENUM : struct, IConvertible
    {
        public readonly FilePosition filePosition;
        public readonly ENUM key;
        public readonly TYPE value;

        public GenericToken(FilePosition filePosition_, ENUM key_, TYPE value_)
        {
            filePosition = filePosition_;
            key = key_;
            value = value_;
        }
    }
}
