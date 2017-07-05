#region using

using System.Collections.Generic;

#endregion

namespace HBD.Mvc.Core
{
    public class JsonUserRole
    {
        public JsonUserRole(IList<string> roles)
        {
            Roles = roles;
        }

        public string User { get; set; }
        public IList<string> Roles { get; }
    }
}