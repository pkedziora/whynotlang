using System.Collections.Generic;
using System.Threading.Tasks;
using WhyNotLang.Interpreter.Evaluators;
using WhyNotLang.Interpreter.Evaluators.ExpressionValues;
using WhyNotLang.Interpreter.State;
using WhyNotLang.Parser.Statements;

namespace WhyNotLang.Interpreter.StatementExecutors
{
    public class IfStatementExecutor : IStatementExecutor
    {
        private readonly IStatementIterator _statementIterator;
        private readonly IExpressionEvaluator _expressionEvaluator;
        private readonly IProgramState _programState;

        public IfStatementExecutor(IStatementIterator statementIterator, IExpressionEvaluator expressionEvaluator, IProgramState programState)
        {
            _statementIterator = statementIterator;
            _expressionEvaluator = expressionEvaluator;
            _programState = programState;
        }
        
        public async Task<ExpressionValue> Execute()
        {
            var ifStatement = _statementIterator.CurrentStatement as IfStatement;
            var conditionValue = await _expressionEvaluator.Eval(ifStatement.Condition);

            IStatement statementToExecute;
            if ((int) conditionValue.Value != 0)
            {
                statementToExecute = ifStatement.Body;
            }
            else
            {
                statementToExecute = ifStatement.ElseStatement;
            }
            
            var newExecutor = Executor.CreateExecutor(new List<IStatement> {statementToExecute}, _programState);
            await newExecutor.ExecuteAll();
            
            return ExpressionValue.Empty;
        }
    }
}