using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using WhyNotLang.Interpreter.Evaluators.ExpressionValues;
using WhyNotLang.Interpreter.State;
using Xunit;

namespace WhyNotLang.Interpreter.Tests
{
    public class ReturnStatementTests
    {
        private IProgramState _programState;
        private IExecutor _executor;

        public ReturnStatementTests()
        {
            var serviceProvider = IoC.BuildServiceProvider();
            _executor = serviceProvider.GetService<IExecutor>();
            _programState = serviceProvider.GetService<IProgramState>();
        }

        [Fact]
        public async Task ExecutesFunctionWithoutParamsWithSimpleReturn()
        {
            _executor.Initialise(@"
                function foo()
                begin
                    return 1 
                end
                var x:= foo()         
            ");
            
            await _executor.ExecuteAll();
            
            var actual = _programState.GetVariable("x");

            var expected = new ExpressionValue(1, ExpressionValueTypes.Number);

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public async Task ExecutesFunctionWithParamsAndReturnStatementWithExpression()
        {
            _executor.Initialise(@"
                function foo(y)
                begin
                    var x:= y + 1
                    return x + 1
                end
                var z:= foo(100)         
            ");
            
            await _executor.ExecuteAll();
            
            var actual = _programState.GetVariable("z");

            var expected = new ExpressionValue(102, ExpressionValueTypes.Number);

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public async Task ExecutesFunctionWith2ParamsAndReturnStatementWithExpression()
        {
            _executor.Initialise(@"
                function foo(x,y)
                begin
                    return x * y
                end
                var z:= foo(2,3)         
            ");
            
            await _executor.ExecuteAll();
            
            var actual = _programState.GetVariable("z");

            var expected = new ExpressionValue(6, ExpressionValueTypes.Number);

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public async Task ReturnStatementStopsExecutionInFunction()
        {
            _executor.Initialise(@"
                function foo()
                begin
                    return 1
                    var y:= 100
                end
                var x:= foo()         
            ");
            
            await _executor.ExecuteAll();
            
            var actual = _programState.GetVariable("x");

            var expected = new ExpressionValue(1, ExpressionValueTypes.Number);

            Assert.Equal(expected, actual);
            Assert.False(_programState.IsVariableDefined("y"));
        }
        
        [Fact]
        public async Task ReturnStatementStopsExecutionOutsideFunction()
        {
            _executor.Initialise(@"
                var x := 100
                return 0
                var y := 2
            ");
            
            await _executor.ExecuteAll();
            
            var actual = _programState.GetVariable("x");

            var expected = new ExpressionValue(100, ExpressionValueTypes.Number);

            Assert.Equal(expected, actual);
            Assert.False(_programState.IsVariableDefined("y"));
        }
    }
}