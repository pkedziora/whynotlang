using System.Linq;
using Xunit;

namespace WhyNotLang.Tokenizer.Tests
{
    public class SingleTokenTests
    {
        private readonly Tokenizer _tokenizer;
        public SingleTokenTests()
        {
            _tokenizer = new Tokenizer(new TokenReader(), new TokenFactory());
        }

        [Theory]
        [InlineData("var", TokenType.Var)]
        [InlineData("begin", TokenType.Begin)]
        [InlineData("end", TokenType.End)]
        [InlineData("if", TokenType.If)]
        [InlineData("else", TokenType.Else)]
        [InlineData("while", TokenType.While)]
        [InlineData("func", TokenType.Function)]
        [InlineData("return", TokenType.Return)]
        [InlineData(",", TokenType.Comma)]
        [InlineData("(", TokenType.LeftParen)]
        [InlineData(")", TokenType.RightParen)]
        [InlineData("+", TokenType.Plus)]
        [InlineData("-", TokenType.Minus)]
        [InlineData("*", TokenType.Multiply)]
        [InlineData("/", TokenType.Divide)]
        [InlineData("!", TokenType.Not)]
        [InlineData(":=", TokenType.Assign)]
        [InlineData("<", TokenType.LessThan)]
        [InlineData(">", TokenType.GreaterThan)]
        [InlineData("<=", TokenType.LessThanOrEqual)]
        [InlineData(">=", TokenType.GreaterThanOrEqual)]
        [InlineData("==", TokenType.Equal)]
        [InlineData("!=", TokenType.NotEqual)]
        [InlineData("1234", TokenType.Number)]
        [InlineData("xyz", TokenType.Identifier)]
        [InlineData("foo", TokenType.Identifier)]
        [InlineData("\"bar\"", TokenType.String)]
        [InlineData("$", TokenType.Invalid)]
        [InlineData("[", TokenType.LeftBracket)]
        [InlineData("]", TokenType.RightBracket)]
        [InlineData("global", TokenType.Global)]
        public void RecognizesSingleTokens(string tokenStr, TokenType expected)
        {
            var actual = _tokenizer.GetTokens(tokenStr);
            var expectedValue = tokenStr.Replace("\"", "");
            var actualToken = actual.First();
            Assert.Equal(1, actual.Count);
            Assert.Equal(expected, actualToken.Type);
            Assert.Equal(expectedValue, actualToken.Value);
        }
    }
}