using System;

namespace M99.Errors
{
    public class M99Error : Exception
    {
        public M99Error(string message) : base(message)
        {
        }
    }
}
