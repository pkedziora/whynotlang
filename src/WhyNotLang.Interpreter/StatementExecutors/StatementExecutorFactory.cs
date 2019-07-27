using System;
using WhyNotLang.Interpreter.Evaluators;
using WhyNotLang.Interpreter.State;
using WhyNotLang.Parser.Statements;

namespace WhyNotLang.Interpreter.StatementExecutors
{
    public class StatementExecutorFactory : IStatementExecutorFactory
    {
        private readonly IStatementIterator _statementIterator;
        private readonly IExpressionEvaluator _expressionEvaluator;
        private readonly IProgramState _programState;

        public StatementExecutorFactory(IStatementIterator statementIterator, IExpressionEvaluator expressionEvaluator, IProgramState programState)
        {
            _statementIterator = statementIterator;
            _expressionEvaluator = expressionEvaluator;
            _programState = programState;
        }

        public IStatementExecutor CreateStatementExecutor()
        {
            switch (_statementIterator.CurrentStatement.Type)
            {
                case StatementType.VariableDeclarationStatement:
                    return new VariableDeclarationExecutor(_statementIterator, _expressionEvaluator, _programState);
                case StatementType.VariableAssignmentStatement:
                    return new VariableAssignmentExecutor(_statementIterator, _expressionEvaluator, _programState);
                case StatementType.FunctionDeclarationStatement:
                    return new FunctionDeclarationStatementExecutor(_statementIterator, _programState);
                case StatementType.IfStatement:
                    return new IfStatementExecutor(_statementIterator, _expressionEvaluator, _programState);
                case StatementType.BlockStatement:
                    return new BlockStatementExecutor(_statementIterator, _programState);
                case StatementType.WhileStatement:
                    return new WhileStatementExecutor(_statementIterator, _expressionEvaluator, _programState);
                case StatementType.FunctionCallStatement:
                    return new FunctionCallStatementExecutor(_statementIterator, _expressionEvaluator, _programState);
                case StatementType.ReturnStatement:
                    return new ReturnStatementExecutor(_statementIterator, _expressionEvaluator, _programState);
                case StatementType.ArrayDeclarationStatement:
                    return new ArrayDeclarationExecutor(_statementIterator, _expressionEvaluator, _programState);
                case StatementType.ArrayAssignmentStatement:
                    return new ArrayAssignmentExecutor(_statementIterator, _expressionEvaluator, _programState);
                case StatementType.EmptyStatement:
                    return new EmptyExecutor();
            }
            
            throw new ArgumentException("Executor not found for current statement");
        }
    }
}