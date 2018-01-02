#region using

using HBD.Framework;
using System;

#endregion

namespace HBD.Mef.Console.Core
{
    public static class ParameterParser
    {
        public static PluginExecutionCollection Parse(params string[] args)
        {
            var coll = new PluginExecutionCollection();
            PluginExecution m = null;

            foreach (var p in args)
            {
                var a = p.Trim();
                if (a.IsNullOrEmpty()) continue;

                if (a.StartsWith("-", StringComparison.Ordinal))
                {
                    m = new PluginExecution { Name = a.Replace("-", string.Empty) };
                    coll.Add(m);
                }
                else
                {
                    m?.Parameters.Add(a);
                }
            }

            return coll;
        }
    }
}