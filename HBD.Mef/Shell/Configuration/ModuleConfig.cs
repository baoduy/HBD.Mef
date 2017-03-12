#region using

using System.Collections.Generic;
using HBD.Framework.Core;
using Newtonsoft.Json;

#endregion

namespace HBD.Mef.Shell.Configuration
{
    public class ModuleConfig : NotifyPropertyChange
    {
        private bool _allowToManage = true;
        private string _description = "N/A";
        private bool _isEnabled = true;
        private string _name = "N/A";
        private string _version;

        /// <summary>
        ///     The name of Module
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }

        /// <summary>
        ///     The description of Module
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { SetValue(ref _description, value); }
        }

        /// <summary>
        ///     The version of module. This should not be null.
        /// </summary>
        public string Version
        {
            get { return _version; }
            set { SetValue(ref _version, value); }
        }

        /// <summary>
        ///     The costume Assemblies that will be loaded into Mef. If this list is empty whole folders
        ///     will be loaded.
        /// </summary>
        public IList<string> AssemplyFiles { get; } = new List<string>();

        /// <summary>
        ///     The flag to Disable or Enable the Module.
        /// </summary>
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetValue(ref _isEnabled, value); }
        }

        /// <summary>
        ///     Allows this Module to be managed by using Module Manager.
        /// </summary>
        public bool AllowToManage
        {
            get { return _allowToManage; }
            set { SetValue(ref _allowToManage, value); }
        }

        [JsonIgnore]
        public string Directory { get; set; }

        [JsonIgnore]
        public string ConfigFile { get; set; }

        [JsonIgnore]
        public string InValidMessage { get; set; }

        [JsonIgnore]
        public bool IsValid { get; set; } = true;
    }
}