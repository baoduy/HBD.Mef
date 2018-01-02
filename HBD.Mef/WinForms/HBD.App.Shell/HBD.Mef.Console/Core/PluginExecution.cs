#region using

using System.Collections.Generic;

#endregion

namespace HBD.Mef.Console.Core
{
    public sealed class PluginExecution
    {
        internal PluginExecution()
        {
        }

        public string Name { get; set; }
        public IList<string> Parameters { get; } = new List<string>();
    }
}