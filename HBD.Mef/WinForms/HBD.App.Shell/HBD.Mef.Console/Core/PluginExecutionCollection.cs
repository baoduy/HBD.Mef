#region using

using System.Collections.Generic;
using System.Linq;

#endregion

namespace HBD.Mef.Console.Core
{
    public sealed class PluginExecutionCollection : List<PluginExecution>
    {
        internal PluginExecutionCollection()
        {
        }

        public PluginExecution this[string name] => this.FirstOrDefault(p => p.Name == name);
    }
}