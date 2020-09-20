namespace M99.Errors
{
    public class M99AllocationError : M99Error
    {
        public readonly int Address;

        public M99AllocationError(int address, string message) : base("{address}\t{message}")
        {
            Address = address;
        }
    }
}
