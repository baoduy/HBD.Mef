using HBD.Mef.Shell.Configuration;
// <copyright file="ShellConfigManagerFactory.cs" company="Duy Hoang">Copyright © Microsoft 2016</copyright>

using System;
using Microsoft.Pex.Framework;

namespace HBD.Mef.Shell.Configuration
{
    /// <summary>A factory for HBD.Mef.Shell.Configuration.ShellConfigManager instances</summary>
    public static partial class ShellConfigManagerFactory
    {
        /// <summary>A factory for HBD.Mef.Shell.Configuration.ShellConfigManager instances</summary>
        [PexFactoryMethod(typeof(ShellConfigManager))]
        public static ShellConfigManager Create()
        {
            ShellConfigManager shellConfigManager = new ShellConfigManager();
            return shellConfigManager;

            // TODO: Edit factory method of ShellConfigManager
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
