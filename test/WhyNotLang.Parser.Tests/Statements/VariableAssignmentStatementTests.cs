using Microsoft.Extensions.DependencyInjection;
using WhyNotLang.Interpreter;
using WhyNotLang.Parser.Expressions;
using WhyNotLang.Parser.Statements;
using WhyNotLang.Test.Common;
using WhyNotLang.Tokenizer;
using Xunit;

namespace WhyNotLang.Parser.Tests.Statements
{
    public class VariableAssignmentStatementTests
    {
        private readonly IParser _parser;
        public VariableAssignmentStatementTests()
        {
            var serviceProvider = IoC.BuildServiceProvider();
            _parser = serviceProvider.GetService<IParser>();
        }

        [Fact]
        public void ParsesAssignmentWithSingleNumber()
        {
            _parser.Initialise("x := 1");
            var expected = new VariableAssignmentStatement(
                new Token(TokenType.Identifier, "x"),
                new ValueExpression(new Token(TokenType.Number, "1")));

            var actual = _parser.ParseNext();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParsesAssignmentWithString()
        {
            _parser.Initialise("x := \"abc\"");
            var expected = new VariableAssignmentStatement(
                new Token(TokenType.Identifier, "x"),
                new ValueExpression(new Token(TokenType.String, "abc")));

            var actual = _parser.ParseNext();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParsesAssignmentWith2PartExpression()
        {
            _parser.Initialise("x := 1 + 2");
            var expected = new VariableAssignmentStatement(
                new Token(TokenType.Identifier, "x"),
                TestHelpers.GetBinaryExpression(1, "+", 2));

            var actual = _parser.ParseNext();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParsesAssignmentWith2PartExpressionWithIdentifiers()
        {
            _parser.Initialise("x := a + b");
            var expected = new VariableAssignmentStatement(
                new Token(TokenType.Identifier, "x"),
                TestHelpers.GetBinaryExpressionWithIdentifiers("a", "+", "b"));

            var actual = _parser.ParseNext();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParsesAssignmentWith3PartExpression()
        {
            _parser.Initialise("x := 1 + 2 * 3");
            var inner = TestHelpers.GetBinaryExpression(2, "*", 3);
            var expression = TestHelpers.GetBinaryExpression(1, "+", inner);
            var expected = new VariableAssignmentStatement(
                new Token(TokenType.Identifier, "x"),
                expression);

            var actual = _parser.ParseNext();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParsesAssignmentWith3PartExpressionWithParens()
        {
            _parser.Initialise("x := (1 + 2) * 3");
            var inner = TestHelpers.GetBinaryExpression(1, "+", 2);
            var expression = TestHelpers.GetBinaryExpression(inner, "*", 3);
            var expected = new VariableAssignmentStatement(
                new Token(TokenType.Identifier, "x"),
                expression);

            var actual = _parser.ParseNext();

            Assert.Equal(expected, actual);
        }
    }
}