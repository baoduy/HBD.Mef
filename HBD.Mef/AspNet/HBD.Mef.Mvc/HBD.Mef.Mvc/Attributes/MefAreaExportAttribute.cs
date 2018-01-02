#region using

using System;
using System.ComponentModel.Composition;
using HBD.Mef.Mvc.Core;

#endregion

namespace HBD.Mef.Mvc.Attributes
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class MefAreaExportAttribute : ExportAttribute
    {
        public MefAreaExportAttribute() : base(typeof(IMefAreaRegistration))
        {
        }
    }
}