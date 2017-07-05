#region using

using System.Collections.Generic;
using HBD.Mef.Mvc.Core;

#endregion

namespace HBD.Mef.Mvc.Navigation.NavigateInfo
{
    public class MenuComparer : IComparer<IMenuInfo>
    {
        public int Compare(IMenuInfo x, IMenuInfo y)
        {
            var d1 = x as IDisplayability;
            var d2 = y as IDisplayability;

            if (d1 == null && d2 == null)
                return 0;
            if (d1 == null)
                return 1;
            if (d2 == null)
                return -1;

            if (d1.DisplayIndex == null)
                d1.DisplayIndex = ushort.MaxValue;
            if (d2.DisplayIndex == null)
                d2.DisplayIndex = ushort.MaxValue;

            return Comparer<ushort?>.Default.Compare(d1.DisplayIndex, d2.DisplayIndex);
        }
    }
}