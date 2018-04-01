#region

using HBD.Mef.Mvc.Navigation.NavigateInfo;

#endregion

namespace HBD.Mef.Mvc.Core
{
    public abstract class DisplayabilityBase : IDisplayability
    {
        internal static readonly ushort StartDisplayIndex = ushort.MaxValue / 2;
        private static ushort _currentDisplayIndex = StartDisplayIndex;

        protected DisplayabilityBase()
        {
            DisplayIndex = GetNextDisplayIndex();
        }

        public MenuDisplayType DisplayType { get; set; } = MenuDisplayType.IconAndText;
        public MenuAlignment Alignment { get; set; } = MenuAlignment.Left;
        public ushort DisplayIndex { get; set; }

        private static ushort GetNextDisplayIndex()
        {
            return _currentDisplayIndex++;
        }
    }
}