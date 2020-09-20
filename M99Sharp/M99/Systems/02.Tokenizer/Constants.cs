using M99Sharp.M99.Systems.Classifier;
using System.Collections.Generic;

namespace M99Sharp.M99.Systems.Tokenizer
{
    public static class Constants
    {
        public static readonly Dictionary<ClassifierType, TokenizerType> conversionTable = new Dictionary<ClassifierType, TokenizerType>()
        {
            { ClassifierType.NONE, TokenizerType.NONE },
            { ClassifierType.SPACE, TokenizerType.NONE },
            { ClassifierType.COLUMN, TokenizerType.COLUMN },
            { ClassifierType.COMMA, TokenizerType.COMMA },
            { ClassifierType.NUMERAL, TokenizerType.NUMERAL },
            { ClassifierType.LITTERAL, TokenizerType.LITTERAL },
            { ClassifierType.PLUS, TokenizerType.PLUS },
            { ClassifierType.DASH, TokenizerType.DASH },
            { ClassifierType.SLASH, TokenizerType.SLASH },
            { ClassifierType.STAR, TokenizerType.STAR },
            { ClassifierType.NEW_LINE, TokenizerType.NEW_LINE },
        };
    }
}
