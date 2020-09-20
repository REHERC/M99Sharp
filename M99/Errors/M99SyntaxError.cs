using M99.Systems.Tokenizer;
using System;
using System.Linq;

namespace M99.Errors
{
    public class M99SyntaxError : M99Error
    {
        public Token Token { get; private set; } = Token.Empty;

        public TokenType Expected  { get; private set; } = TokenType.NONE;

        public M99SyntaxError(Token cause) : base($"Unexpected token \"{cause.value}\" at {cause.position.Debug()}")
        {
            Token = cause;
        }

        public M99SyntaxError(Token cause, params TokenType[] expected) : base($"Unexpected token \"{cause.value}\" at {cause.position.Debug()}\tExpected one of the following types: {string.Join(", ", expected)}")
        {
            Token = cause;
        }
    }
}
