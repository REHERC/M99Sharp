using System;
using System.Linq;

public static class EnumExtensions
{
    public static T GetEnum<T>(this T e, string litteral) where T : struct, IConvertible
    {
        return GetEnum<T>(litteral);
    }

    public static T GetEnum<T>(string litteral) where T : struct, IConvertible
    {
        return Enum.GetValues(typeof(T)).Cast<T>().Where(x => string.Equals(x.ToString(), litteral, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
    }
}
