using System;
using HBD.Mef.Console;
using System.ComponentModel.Composition;
using HBD.Mef.Modularity;

namespace TestModule1
{
    [PluginExport("Job1", typeof(Job1))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class Job1 : ConsolePluginBase
    {
        public override void Run(params string[] args)
        {
            Console.WriteLine($"Run {this.GetType().FullName} with Parameters: {string.Join(",", args)}");
        }
    }
}
