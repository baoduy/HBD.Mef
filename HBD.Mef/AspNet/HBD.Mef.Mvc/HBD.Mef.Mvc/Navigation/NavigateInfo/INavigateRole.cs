#region

using System.Collections.Generic;

#endregion

namespace HBD.Mef.Mvc.Navigation.NavigateInfo
{
    public interface INavigateRole
    {
        /// <summary>
        ///     The navigation will be displayed for authorized users.
        /// </summary>
        bool ReguireAuthorized { get; set; }

        /// <summary>
        ///     The navigation will be displayed once users in this Roles.
        /// </summary>
        IList<string> Roles { get; }
    }
}