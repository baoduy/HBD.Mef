using System;

namespace HBD.Mef.Modularity
{
    public interface IModuleInfo
    {
        Guid Id { get; }
        string Name { get; }
        string Version { get; }
        string Description { get; }

        /// <summary>
        /// The system module is not allow to be Disabled.
        /// </summary>
        bool IsSystemModule { get; }
    }
}
