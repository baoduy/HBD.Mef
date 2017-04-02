using System;
using System.Collections.ObjectModel;

namespace HBD.Mef.Modularity
{
    [Serializable]
    public class ModuleInfo
    {
        public string ModuleName { get; set; }

        public Type ModuleType { get; set; }

        public Collection<string> DependsOn { get; set; }

        public ModuleState State { get; set; }

        public ModuleInfo()
          : this(null, null, new string[0])
        {
        }

        public ModuleInfo(string name, Type type, params string[] dependsOn)
        {
            if (dependsOn == null)
                throw new ArgumentNullException(nameof(dependsOn));
            ModuleName = name;
            ModuleType = type;
            DependsOn = new Collection<string>();
            foreach (string str in dependsOn)
                DependsOn.Add(str);
        }

        public ModuleInfo(string name, Type type)
          : this(name, type, new string[0])
        {
        }
    }
}
