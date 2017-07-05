using System.Collections.Generic;
using System.Reflection;
using System.Composition.Convention;

namespace HBD.Mef.Catalogs
{
    public interface ICatalog
    {
        IReadOnlyCollection<Assembly> Assemblies { get;  }
        AttributedModelProvider Provider { get; }
    }
}
