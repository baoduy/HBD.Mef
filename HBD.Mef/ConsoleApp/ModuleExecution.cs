using System.Collections.Generic;

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