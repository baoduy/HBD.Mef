using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBD.Mef.Mvc.Core
{
    public interface IAreaBundle
    {
        string Path { get; }
        string AreaName { get; }
        string BundleName { get; }
    }
}
