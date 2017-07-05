using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using HBD.Mef.Mvc.Services;

namespace HBD.Mef.Mvc.Catalogs
{
    /// <summary>
    /// This catalog will helps to export all the controllers that had been marked ad [Export]
    /// </summary>
    public class MvcExportControllersCatalog : DefaultMvcBinaryFoldersCatalog
    {
        public override string DisplayName => "Export all default controllers.";

        public MvcExportControllersCatalog(ICustomBinaryManager customBinaryManager) : base(customBinaryManager)
        {
        }

        public override IEnumerator<ComposablePartDefinition> GetEnumerator()
        {
            return base.GetEnumerator();
        }

        public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
        {
            return base.GetExports(definition);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }
    }
}
