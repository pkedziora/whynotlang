using System.Linq;
using WhyNotLang.Parser.Expressions;
using WhyNotLang.Parser.Statements;
using WhyNotLang.Tokenizer;

namespace WhyNotLang.Parser.Tests
{
    public class TestHelpers
    {
        private static readonly Tokenizer.Tokenizer _tokenizer = CreateTokenizer();

        public static TokenIterator CreateTokenIterator()
        {
            return new TokenIterator(new Tokenizer.Tokenizer(new TokenReader(), new TokenMap()));
        }

        public static ExpressionParser CreateExpressionParser(ITokenIterator tokenIterator = null)
        {
            tokenIterator = tokenIterator ??
                            new TokenIterator(new Tokenizer.Tokenizer(new TokenReader(), new TokenMap()));
            return new ExpressionParser(tokenIterator);
        }
        
        public static Parser CreateParser()
        {
            var tokenIterator = CreateTokenIterator();
            var expressionParser = CreateExpressionParser(tokenIterator);
            return new Parser(tokenIterator, new StatementParserMap(tokenIterator, expressionParser));
        }
        
        public static BinaryExpression GetBinaryExpression(int a, string op, int b)
        {
            var left = new ValueExpression(new Token(TokenType.Number, a.ToString()));
            var right = new ValueExpression(new Token(TokenType.Number, b.ToString()));
            return new BinaryExpression(left, GetToken(op), right);
        }
        
        public static BinaryExpression GetBinaryExpression(string a, string op, string b)
        {
            var left = new ValueExpression(new Token(TokenType.Identifier, a));
            var right = new ValueExpression(new Token(TokenType.Identifier, b));
            return new BinaryExpression(left, GetToken(op), right);
        }
        
        public static BinaryExpression GetBinaryExpression(IExpression left, string op, int b)
        {
            var right = new ValueExpression(new Token(TokenType.Number, b.ToString()));
            return new BinaryExpression(left, GetToken(op), right);
        }
        
        public static BinaryExpression GetBinaryExpression(IExpression left, string op, string b)
        {
            var right = new ValueExpression(new Token(TokenType.Identifier, b));
            return new BinaryExpression(left, GetToken(op), right);
        }
        
        public static BinaryExpression GetBinaryExpression(int a, string op, IExpression right)
        {
            var left = new ValueExpression(new Token(TokenType.Number, a.ToString()));
            return new BinaryExpression(left, GetToken(op), right);
        }
        
        public static BinaryExpression GetBinaryExpression(string a, string op, IExpression right)
        {
            var left = new ValueExpression(new Token(TokenType.Identifier, a));
            return new BinaryExpression(left, GetToken(op), right);
        }
        
        public static BinaryExpression GetBinaryExpression(IExpression left, string op, IExpression right)
        {
            return new BinaryExpression(left, GetToken(op), right);
        }

        public static UnaryExpression GetUnaryExpression(string op, int number)
        {
            return new UnaryExpression(new ValueExpression(TestHelpers.GetToken(number.ToString())), GetToken(op));
        }
        
        public static UnaryExpression GetUnaryExpression(string op, string identifier)
        {
            return new UnaryExpression(new ValueExpression(TestHelpers.GetToken(identifier)), GetToken(op));
        }
        
        public static UnaryExpression GetUnaryExpression(string op, IExpression inner)
        {
            return new UnaryExpression(inner, GetToken(op));
        }

        public static Token GetToken(string token)
        {
            return _tokenizer.GetTokens(token).FirstOrDefault();
        }

        private static Tokenizer.Tokenizer CreateTokenizer()
        {
            return new Tokenizer.Tokenizer(new TokenReader(), new TokenMap());
        }
    }
}