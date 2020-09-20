using M99Sharp.M99.Models;

namespace M99Sharp.M99.Systems.Tokenizer
{
    public sealed class TokenizerToken : GenericToken<TokenizerType, string>
    {
        public static TokenizerToken Empty => new TokenizerToken(FilePosition.Empty, TokenizerType.NONE, string.Empty);

        public TokenizerToken(FilePosition fp, TokenizerType ct, string s) : base(fp, ct, s)
        {

        }
    }
}
