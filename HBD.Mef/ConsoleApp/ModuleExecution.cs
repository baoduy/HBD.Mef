#region

using System.Collections.Generic;

#endregion

namespace HBD.Mef.ConsoleApp
{
    public sealed class ModuleExecution
    {
        internal ModuleExecution()
        {
        }

        public string Name { get; set; }
        public IList<string> Parameters { get; } = new List<string>();
    }
}