#region using

using System;
using System.IO;

#endregion

namespace HBD.WinForms.Shell
{
    internal static class Functions
    {
        public static string GetIconPath(string icon)
        {
            if (File.Exists(icon)) return icon;

            icon = $"{AppDomain.CurrentDomain.BaseDirectory}\\{icon}";
            return File.Exists(icon) ? icon : string.Empty;
        }
    }
}