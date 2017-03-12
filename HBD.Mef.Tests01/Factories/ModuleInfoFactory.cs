using Prism.Modularity;
using System;
using Microsoft.Pex.Framework;

namespace Prism.Modularity
{
    /// <summary>A factory for Prism.Modularity.ModuleInfo instances</summary>
    public static partial class ModuleInfoFactory
    {
        /// <summary>A factory for Prism.Modularity.ModuleInfo instances</summary>
        [PexFactoryMethod(typeof(ModuleInfo))]
        public static ModuleInfo Create(
            string name_s,
            string type_s1,
            string[] dependsOn_ss,
            InitializationMode value_i,
            string value_s2,
            ModuleState value_i1_
        )
        {
            ModuleInfo moduleInfo = new ModuleInfo(name_s, type_s1, dependsOn_ss);
            moduleInfo.InitializationMode = value_i;
            moduleInfo.Ref = value_s2;
            moduleInfo.State = value_i1_;
            return moduleInfo;

            // TODO: Edit factory method of ModuleInfo
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
