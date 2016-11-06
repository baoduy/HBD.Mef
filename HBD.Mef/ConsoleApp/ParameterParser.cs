using System;

namespace HBD.Mef.ConsoleApp
{
    public static class ParameterParser
    {
        public static ModuleExecutionCollection Parse(params string[] args)
        {
            var coll = new ModuleExecutionCollection();
            ModuleExecution m = null;

            foreach (var p in args)
                if (p.StartsWith("-", StringComparison.Ordinal))
                {
                    m = new ModuleExecution {Name = p.Replace("-", string.Empty)};
                    coll.Add(m);
                }
                else
                {
                    m?.Parameters.Add(p);
                }

            return coll;
        }
    }
}