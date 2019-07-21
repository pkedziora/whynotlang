using System;
using WhyNotLang.Interpreter.ExpressionValues;
using WhyNotLang.Parser.Expressions;

namespace WhyNotLang.Interpreter.Evaluators
{
    public class ExpressionEvaluator : IExpressionEvaluator
    {
        public ExpressionValue Eval(IExpression expression)
        {
            var evaluator = GetExpressionEvaluator(expression);
            return evaluator.Eval(expression);
        }
        
        public IExpressionEvaluator GetExpressionEvaluator(IExpression expression)
        {
            switch (expression.Type)
            {
                case ExpressionType.Value:
                    return new ValueExpressionEvaluator();
            }
            
            throw new ArgumentException("Parser not found for current token");
        }
    }
}