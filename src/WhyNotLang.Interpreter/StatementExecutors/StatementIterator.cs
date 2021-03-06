using System.Collections.Generic;
using WhyNotLang.Parser.Statements;

namespace WhyNotLang.Interpreter.StatementExecutors
{
    public class StatementIterator : IStatementIterator
    {
        public IStatement CurrentStatement => _currentIndex < _statements.Count ? _statements[_currentIndex] : new EofStatement();
        private List<IStatement> _statements;
        private int _currentIndex;

        public StatementIterator()
        {
        }

        public StatementIterator(List<IStatement> statements)
        {
            _statements = statements;
            _currentIndex = 0;
        }

        public void InitStatements(List<IStatement> statements)
        {
            _statements = statements;
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

        public void ResetPosition()
        {
            _currentIndex = 0;
        }
    }
}