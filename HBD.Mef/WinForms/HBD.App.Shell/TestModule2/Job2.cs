using HBD.Mef.Console;
using System;

namespace TestModule2
{
    public class Job2 : ConsolePluginBase
    {
        public override void Run(params string[] args)
        {
            Console.WriteLine($"Run {this.GetType().FullName} with Parameters: {string.Join(",", args)}");
        }
    }
}
