#region

using System.ComponentModel;
using HBD.Mef.Mvc.Navigation.NavigateInfo;

#endregion

namespace HBD.Mef.Mvc.Core
{
    public interface IDisplayability
    {
        /// <summary>
        ///     Default value is Left
        /// </summary>
        [DefaultValue(MenuAlignment.Left)]
        MenuAlignment Alignment { get; set; }

        /// <summary>
        ///     Default value is IconAndText
        /// </summary>
        [DefaultValue(MenuDisplayType.IconAndText)]
        MenuDisplayType DisplayType { get; set; }

        /// <summary>
        ///     The display index of the menu.
        ///     Default value must be ushort.MaxValue.
        /// </summary>
        [DefaultValue(ushort.MaxValue)]
        ushort DisplayIndex { get; set; }
    }
}