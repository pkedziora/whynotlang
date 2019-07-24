﻿using System;
using System.IO;
using WhyNotLang.Interpreter.State;

namespace WhyNotLang.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName;
            if(System.Diagnostics.Debugger.IsAttached)
            {
                fileName = "Samples/Test.wnl";
            }
            else
            {
                if (args.Length != 1)
                {
                    Console.WriteLine("Plese provide path to the program to execute");
                    return;
                }
                
                fileName = args[0];
            }


            var programState = new ProgramState();
            var executor = ExecutorFactory.CreateExecutor(programState);

            var program = File.ReadAllText(fileName);
            executor.Initialise(program);
            executor.ExecuteAll();

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.ReadLine();
            }
        }
    }
}