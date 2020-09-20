using M99Sharp.M99.Systems.Tokenizer;
using System.Collections.Generic;

namespace M99Sharp.M99.Systems.Cleaner
{
    public static class Constants
    {
        public static readonly Dictionary<TokenizerType, CleanerType> conversionTable = new Dictionary<TokenizerType, CleanerType>()
        {
            { TokenizerType.NONE, CleanerType.NONE },
            { TokenizerType.SPACE, CleanerType.NONE },
            { TokenizerType.COLUMN, CleanerType.COLUMN },
            { TokenizerType.COMMA, CleanerType.COMMA },
            { TokenizerType.NUMERAL, CleanerType.NUMERAL },
            { TokenizerType.LITTERAL, CleanerType.LITTERAL },
            { TokenizerType.PLUS, CleanerType.PLUS },
            { TokenizerType.DASH, CleanerType.DASH },
            { TokenizerType.SLASH, CleanerType.SLASH },
            { TokenizerType.STAR, CleanerType.STAR },
            { TokenizerType.NEW_LINE, CleanerType.NEW_LINE },
        };
    }
}
