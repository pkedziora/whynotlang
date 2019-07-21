using System.Collections.Generic;
using WhyNotLang.Parser;
using WhyNotLang.Parser.Statements;
using WhyNotLang.Tokenizer;

namespace WhyNotLang.Interpreter.StatementExecutors
{
    public class StatementIterator : IStatementIterator
    {
        public IStatement CurrentStatement => _currentIndex < _statements.Count ? _statements[_currentIndex] : new EofStatement();
        private List<IStatement> _statements;
        private int _currentIndex;
        private readonly IParser _parser;
        
        public void InitStatements(string program)
        {
            _statements = _parser.ParseAll();
            _currentIndex = 0;
        }

        public IStatement GetNextStatement()
        {
            if (_currentIndex >= _statements.Count - 1)
            {
                _currentIndex = _statements.Count;
                return new EofStatement();
            }

            _currentIndex++;
            return _statements[_currentIndex];
        }

        public IStatement PeekStatement(int offset)
        {
            var peekIndex = _currentIndex + offset;
            if (peekIndex >= _statements.Count)
            {
                return new EofStatement();
            }
            
            return _statements[peekIndex];
        }
    }
}