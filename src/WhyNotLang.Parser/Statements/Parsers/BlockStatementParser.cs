using System.Collections.Generic;
using WhyNotLang.Tokenizer;

namespace WhyNotLang.Parser.Statements.Parsers
{
    public class BlockStatementParser : IStatementParser
    {
        private readonly ITokenIterator _tokenIterator;
        private readonly IParser _parser;

        public BlockStatementParser(ITokenIterator tokenIterator, IParser parser)
        {
            _tokenIterator = tokenIterator;
            _parser = parser;
        }

        public IStatement Parse()
        {
            var lineNumber = _tokenIterator.CurrentToken.LineNumber;
            if (_tokenIterator.CurrentToken.Type != TokenType.Begin)
            {
                throw new WhyNotLangException("begin expected", lineNumber);
            }

            _tokenIterator.GetNextToken(); // Swallow begin
            var childStatements = new List<IStatement>();
            while (_tokenIterator.CurrentToken.Type != TokenType.End)
            {
                var currentChildStatement = _parser.ParseNext();
                childStatements.Add(currentChildStatement);
            }

            _tokenIterator.GetNextToken(); // Swallow end

            return new BlockStatement(childStatements, lineNumber);
        }
    }
}