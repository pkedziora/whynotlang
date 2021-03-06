using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using WhyNotLang.Interpreter.Builtin;
using WhyNotLang.Interpreter.Evaluators.ExpressionValues;
using WhyNotLang.Tokenizer;

namespace WhyNotLang.Cmd
{
    public static class ServiceProviderExtensions
    {
        public static void AddConsoleInputOutput(this IServiceProvider serviceProvider)
        {
            var functionCollection = serviceProvider.GetRequiredService<IBuiltinFunctionCollection>();
            functionCollection.Add("Writeln",
                async arguments =>
                {
                    var str = arguments.Single();
                    if (str.Type != ExpressionValueTypes.String)
                    {
                        throw new WhyNotLangException("String expected");
                    }

                    Console.WriteLine((string)str.Value);
                    return await Task.FromResult(ExpressionValue.Empty);
                });

            functionCollection.Add("Write",
                async arguments =>
                {
                    var str = arguments.Single();
                    if (str.Type != ExpressionValueTypes.String)
                    {
                        throw new WhyNotLangException("String expected");
                    }

                    Console.Write((string)str.Value);
                    return await Task.FromResult(ExpressionValue.Empty);
                });

            functionCollection.Add("Readln",
                async arguments =>
                {
                    var str = Console.ReadLine();
                    return await Task.FromResult(new ExpressionValue(str, ExpressionValueTypes.String));
                });
        }
    }
}