using M99Sharp.M99.Systems.Tokenizer;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;

namespace M99Sharp.M99.Systems.Cleaner
{
    public class Cleaner
    {
        public readonly TokenizerToken[] source;

        public Cleaner(TokenizerToken[] source_)
        {
            source = source_;
        }

        public CleanerToken[] Clean()
        {
            if (source.Length == 0)
            {
                return new CleanerToken[0];
            }

            CleanerToken[] result;
            TokenizerToken[] processed;

            processed = CleanByType(source, TokenizerType.SPACE, TokenizerType.NONE);
            processed = CleanComments(processed);
            processed = CleanByType(processed, TokenizerType.NEW_LINE, TokenizerType.NONE);
            processed = ProcessNumerals(processed);

            result = processed.Select(item => new CleanerToken(item.filePosition, Constants.conversionTable[item.key], item.value)).ToArray();
            return result;
        }

        private TokenizerToken[] CleanByType(TokenizerToken[] tokens, params TokenizerType[] excludedTypes)
        {
            return tokens.Where(item => !excludedTypes.Contains(item.key)).ToArray();
        }
        
        private TokenizerToken[] CleanComments(TokenizerToken[] tokens)
        {
            if (tokens.Length == 0)
            {
                return new TokenizerToken[0];
            }

            List<TokenizerToken> result = new List<TokenizerToken>();

            CommentType comment = CommentType.NONE;

            for (int index = 0; index < tokens.Length; index++)
            {
                if (index >= tokens.Length)
                {
                    break;
                }

                TokenizerToken currentToken = GetToken(tokens, index);
                TokenizerToken nextToken = GetToken(tokens, index, 1);

                switch (comment)
                {
                    case CommentType.NONE:
                        if (currentToken.key == TokenizerType.SLASH)
                        {
                            if (nextToken.key == TokenizerType.SLASH)
                            {
                                comment = CommentType.SIMPLE;
                            }
                            else if (nextToken.key == TokenizerType.STAR)
                            {
                                comment = CommentType.MULTILINE;
                            }
                        }
                        break;
                    case CommentType.MULTILINE:
                        if (currentToken.key == TokenizerType.STAR && nextToken.key == TokenizerType.SLASH)
                        {
                            comment = CommentType.NONE;
                            index += 2;
                        }
                        break;
                    case CommentType.SIMPLE:
                        if (currentToken.key == TokenizerType.NEW_LINE)
                        {
                            comment = CommentType.NONE;
                        }
                        break;
                }

                if (comment == CommentType.NONE)
                {
                    result.Add(tokens[index]);
                }
            }

            return result.ToArray();
        }

        private TokenizerToken[] ProcessNumerals(TokenizerToken[] tokens)
        {
            if (tokens.Length == 0)
            {
                return new TokenizerToken[0];
            }

            List<TokenizerToken> result = new List<TokenizerToken>();

            TokenizerToken currentToken = TokenizerToken.Empty;
            TokenizerToken nextToken = TokenizerToken.Empty;

            for (int index = 0; index < tokens.Length; index++)
            {
                currentToken = GetToken(tokens, index);
                nextToken = GetToken(tokens, index, 1);

                switch (currentToken.key)
                {
                    case TokenizerType.DASH:
                        if (nextToken.key == TokenizerType.NUMERAL)
                        {
                            result.Add(NegateSign(nextToken));
                            index += 1;
                            continue;
                        }
                        else
                        {
                            break;
                        }
                }

                result.Add(currentToken);
            }

            return result.ToArray();
        }

        public TokenizerToken NegateSign(TokenizerToken token)
        {
            if (token.key == TokenizerType.NUMERAL)
            {
                string newValue;

                if (token.value.Length > 1 && token.value.First() == '-')
                {
                    newValue = token.value.Substring(1);
                }
                else
                {
                    newValue = $"-{token.value}";
                }

                return new TokenizerToken(token.filePosition, token.key, newValue);
            }
            else
            {
                return token;
            }
        }

        public TokenizerToken GetToken(TokenizerToken[] array, int index, int offset = 0)
        {
            int finalIndex = index + offset;

            if (finalIndex < 0 || finalIndex >= array.Length)
            {
                return TokenizerToken.Empty;
            }
            else
            {
                return array[finalIndex];
            }
        }
    }
}
