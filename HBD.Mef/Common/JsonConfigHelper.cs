using HBD.Framework.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.IO;

namespace HBD.Mef.Common
{
    /// <summary>
    ///     Read Configuration from Json file supported caching for subsequent use.
    /// </summary>
    public static class JsonConfigHelper
    {
        private static readonly ConcurrentDictionary<string, object> InternalCacher =
            new ConcurrentDictionary<string, object>();

        private static string GetFilePath(string file)
            => Path.GetFullPath(File.Exists(file) ? file : AppDomain.CurrentDomain.BaseDirectory + "\\" + file);

        public static T ReadConfig<T>(string file) where T : class
        => InternalCacher.GetOrAdd(file, s =>
        {
            var p = GetFilePath(s);
            if (!File.Exists(p)) return null;

            var val = File.ReadAllText(p);
            return JsonConvert.DeserializeObject<T>(val);
        }) as T;

        /// <summary>
        ///     Write config to file the existing file will be rename to [OriginalName]_[yy.mm.dd hhmmss].back
        /// </summary>
        /// <param name="config"></param>
        /// <param name="file"></param>
        public static void SaveConfig(object config, string file)
        {
            Guard.ArgumentIsNotNull(config, nameof(config));
            Guard.ArgumentIsNotNull(file, nameof(file));

            var p = GetFilePath(file);

            //Backup the existing file.
            if (File.Exists(p))
            {
                var backName = Path.GetDirectoryName(p) + "\\" + Path.GetFileNameWithoutExtension(p) + "_" +
                               DateTime.Now.ToString("yy.mm.dd hhmmss") + ".back";
                if (!File.Exists(backName))
                    File.Copy(p, backName);
            }
            //Write data to file.
            var strVal = JsonConvert.SerializeObject(config, Formatting.Indented);
            //Ensure the folder is created
            // ReSharper disable once AssignNullToNotNullAttribute
            System.IO.Directory.CreateDirectory(Path.GetDirectoryName(file));
            File.WriteAllText(p, strVal);
        }
    }
}