#region

using System.Collections.Generic;
using System.ComponentModel;
using HBD.Framework.Core;

#endregion

namespace HBD.Mef.Shell.Configuration
{
    public class ShellConfig : NotifyPropertyChange
    {
        private string _logo;
        private string _name;
        private string _title;

        public ShellConfig()
        {
            ModulePath = "./Modules";
            BackupModulePath = "./BackupModules";
            ThemePath = "./Themes";
        }

        /// <summary>
        ///     The name of the application.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }

        /// <summary>
        ///     The title of Main Form.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetValue(ref _title, value); }
        }

        /// <summary>
        ///     The icon will be displayed on Main Forms icon.
        /// </summary>
        public string Logo
        {
            get { return _logo; }
            set { SetValue(ref _logo, value); }
        }

        /// <summary>
        ///     The Name of Environment if you have multi environment. This one may be display on the
        ///     Main Form title as format {Title} - {Environment}.
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        ///     The location of the Modules that will be loaded when the app start.
        /// </summary>
        [DefaultValue("./Modules")]
        public string ModulePath { get; set; }

        /// <summary>
        ///     The backup location of module.
        /// </summary>
        [DefaultValue("./BackupModules")]
        public string BackupModulePath { get; set; }

        /// <summary>
        ///     The location of Application Themes.
        /// </summary>
        [DefaultValue("./Themes")]
        public string ThemePath { get; set; }

        /// <summary>
        ///     The binaries that will be imported to Mef. If it is just file name then the root folder
        ///     will be from AppDomain.CurrentDomain.BaseDirectory.
        /// </summary>
        public IList<string> ImportedBinaries { get; } = new List<string>();
    }
}