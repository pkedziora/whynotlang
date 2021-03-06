using WhyNotLang.Interpreter.Builtin;
using WhyNotLang.Interpreter.Evaluators.ExpressionValues;
using WhyNotLang.Parser.Statements;

namespace WhyNotLang.Interpreter.State
{
    public interface IProgramState
    {
        IBuiltinFunctionCollection BuiltinFunctionCollection { get; }
        Scope GlobalScope { get; }
        Scope CurrentScope { get; }
        void Clear();
        FunctionDeclarationStatement GetFunction(string identifier);
        void DeclareFunction(string identifier, FunctionDeclarationStatement statement);
        ExpressionValue GetVariable(string identifier);
        bool IsVariableDefined(string identifier);
        bool IsArrayDefined(string identifier);
        bool IsFunctionDefined(string identifier);
        ExpressionValue DeclareVariable(string identifier, ExpressionValue value, bool isGlobal = false);
        void AssignVariable(string identifier, ExpressionValue value);
        Scope AddScope(string name, bool isFunctionScope = false);
        void RemoveScope();
        ExpressionValue GetArrayItem(string identifier, int index);
        void AssignArrayItem(string identifier, int index, ExpressionValue value);
        ExpressionValue[] DeclareArray(string identifier, int size, bool isGlobal = false);
        ExpressionValue GetArrayReference(string identifier);
        void DeclareArrayByReference(string identifier, ExpressionValue expressionValue);
    }
}