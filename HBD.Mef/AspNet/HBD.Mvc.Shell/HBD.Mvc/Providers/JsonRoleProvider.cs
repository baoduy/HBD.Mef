#region using

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Security;
using HBD.Framework.Core;
using HBD.Mvc.Core;
using Newtonsoft.Json;

#endregion

namespace HBD.Mvc.Providers
{
    public class JsonRoleProvider : RoleProvider
    {
        public override string ApplicationName { get; set; }
        public string JsonFile { get; private set; }

        protected IDictionary<string, JsonUserRole> InternaRoles { get; private set; }

        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);

            ApplicationName = config[nameof(ApplicationName)];
            JsonFile = config[nameof(JsonFile)];

            Guard.ArgumentIsNotNull(JsonFile, nameof(JsonFile));

            if (JsonFile.StartsWith("~"))
                JsonFile = HostingEnvironment.MapPath(JsonFile);

            if (!File.Exists(JsonFile))
                throw new ArgumentException($"File {JsonFile} is not found.");

            InternaRoles = new Dictionary<string, JsonUserRole>();

            // ReSharper disable once AssignNullToNotNullAttribute
            foreach (var item in JsonConvert.DeserializeObject<JsonUserRole[]>(File.ReadAllText(JsonFile)))
                InternaRoles.Add(item.User.ToLower(), item);
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return InternaRoles.ContainsKey(GetUserName(username)) &&
                   InternaRoles[GetUserName(username)].Roles.Contains(roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            return InternaRoles.ContainsKey(GetUserName(username))
                ? InternaRoles[GetUserName(username)].Roles.ToArray()
                : new string[] { };
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName,
            bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            return InternaRoles.Values.Any(i => i.Roles.Contains(roleName));
        }

        public override void AddUsersToRoles(string[] usernames,
            string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames,
            string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return InternaRoles.Values.Where(i => i.Roles.Contains(roleName)).Select(i => i.User).ToArray();
        }

        public override string[] GetAllRoles()
        {
            return InternaRoles.Values.SelectMany(a => a.Roles).Distinct().ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return GetUsersInRole(roleName);
        }

        private static string GetUserName(string userName)
        {
            return UserPrincipalHelper.GetUserNameWithoutDomain(userName)?.ToLower();
        }
    }
}