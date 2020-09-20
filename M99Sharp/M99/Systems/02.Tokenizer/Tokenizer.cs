using M99Sharp.M99.Models;
using M99Sharp.M99.Systems.Classifier;
using System;
using System.Collections.Generic;
using System.Linq;

namespace M99Sharp.M99.Systems.Tokenizer
{
    public class Tokenizer
    {
        public readonly ClassifierToken[] source;

        public Tokenizer(ClassifierToken[] source_)
        {
            source = source_;
        }

        public TokenizerToken[] Tokenize()
        {

            if (source.Length == 0)
            {
                return new TokenizerToken[0];
            }

            List<TokenizerToken> result = new List<TokenizerToken>();

            ClassifierType ctLast = ClassifierType.NONE;
            ClassifierType ctCurr = ClassifierType.NONE;

            FilePosition filePosition = source[0].filePosition;

            TokenizerToken tokenizerToken;
            ClassifierToken classifierToken;
            bool forceCut = false;
            string buffer = string.Empty;

            for (int index = 0; index <= source.Length; index++)
            {
                classifierToken = index >= source.Length ? ClassifierToken.Empty : source[index];

                ctCurr = classifierToken.key;

                if (index > 0 && (ctCurr != ctLast || forceCut))
                {
                    if (Constants.conversionTable.TryGetValue(ctLast, out TokenizerType tt))
                    {
                        tokenizerToken = new TokenizerToken(filePosition, tt, buffer);
                    }
                    else
                    {
                        tokenizerToken = new TokenizerToken(filePosition, TokenizerType.NONE, buffer);
                    }

                    result.Add(tokenizerToken);

                    buffer = classifierToken.value.ToString();
                    filePosition = classifierToken.filePosition;
                }
                else
                {
                    buffer += classifierToken.value;
                }

                switch (ctCurr)
                {
                    case ClassifierType.NUMERAL:
                    case ClassifierType.LITTERAL:
                    case ClassifierType.NEW_LINE:
                        forceCut = false;
                        break;
                    default:
                        forceCut = true;
                        break;
                }

                ctLast = ctCurr;
            }

            TokenizerToken[] resultArray = result.ToArray();

            for (int index = 0; index < resultArray.Length; index++)
            {
                ref TokenizerToken item = ref resultArray[index];

                if (item.key == TokenizerType.NEW_LINE)
                {
                    item = new TokenizerToken(new FilePosition(item.filePosition.line - 1), item.key, item.value);
                }
            }

            return resultArray;
        }
    }
}
