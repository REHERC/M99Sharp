using M99Sharp.M99.Models;

namespace M99Sharp.M99.Systems.Cleaner
{
    public sealed class CleanerToken : GenericToken<CleanerType, string>
    {
        public static CleanerToken Empty => new CleanerToken(FilePosition.Empty, CleanerType.NONE, string.Empty);
        public CleanerToken(FilePosition fp, CleanerType ct, string s) : base(fp, ct, s)
        {
        }

        public int GetInteger()
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            else
            {
                // TODO: Implement exception later
                return default;
            }
        }

        public long GetLong()
        {
            if (long.TryParse(value, out long result))
            {
                return result;
            }
            else
            {
                // TODO: Implement exception later
                return default;
            }
        }

        public uint GetUnsignedInteger()
        {
            if (uint.TryParse(value, out uint result))
            {
                return result;
            }
            else
            {
                // TODO: Implement exception later
                return default;
            }
        }

        public short GetShort()
        {
            if (short.TryParse(value, out short result))
            {
                return result;
            }
            else
            {
                // TODO: Implement exception later
                return default;
            }
        }

        public string GetString()
        {
            return value;
        }
    }
}