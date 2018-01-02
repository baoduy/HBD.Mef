#region using

using System;

#endregion

namespace HBD.Mef.Mvc.Navigation.NavigateInfo
{
    public abstract class LazyInfo<TIn, TOut> : NavigationInfoBase
    {
        protected LazyInfo(string areaName) : base(areaName)
        {
        }

        internal Func<TIn, TOut> InfoGetter { get; set; }

        public TOut GetInfo(TIn page)
        {
            return InfoGetter.Invoke(page);
        }
    }
}