#region using

using System.Drawing;
using System.IO;
using HBD.Framework;

#endregion

namespace HBD.Mef.WinForms
{
    internal static class Functions
    {
        public static Image LoadImage(string imagePath)
        {
            if (imagePath.IsNullOrEmpty() || File.Exists(imagePath)) return null;
            return Image.FromFile(imagePath);
        }
    }
}