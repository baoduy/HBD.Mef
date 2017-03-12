#region using

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using Prism.Modularity;

#endregion

namespace HBD.Mef.Modularity
{
    [Export]
    public class MefFileModuleTypeLoader : IModuleTypeLoader
    {
        private const string RefFilePrefix = "file://";
        private readonly HashSet<Uri> _downloadedUris = new HashSet<Uri>();

        [Import(AllowRecomposition = false)]
        public AggregateCatalog AggregateCatalog {protected get; set; }

        public event EventHandler<ModuleDownloadProgressChangedEventArgs> ModuleDownloadProgressChanged;

        public event EventHandler<LoadModuleCompletedEventArgs> LoadModuleCompleted;

        public virtual bool CanLoadModuleType(ModuleInfo moduleInfo)
        {
            if (moduleInfo == null)
                throw new ArgumentNullException(nameof(moduleInfo));
            if (moduleInfo.Ref != null)
                return moduleInfo.Ref.StartsWith("file://", StringComparison.Ordinal);
            return false;
        }

        public virtual void LoadModuleType(ModuleInfo moduleInfo)
        {
            if (moduleInfo == null)
                throw new ArgumentNullException(nameof(moduleInfo));
            try
            {
                var uri = new Uri(moduleInfo.Ref, UriKind.RelativeOrAbsolute);
                if (IsSuccessfullyDownloaded(uri))
                {
                    RaiseLoadModuleCompleted(moduleInfo, null);
                }
                else
                {
                    var str = !moduleInfo.Ref.StartsWith("file:///", StringComparison.Ordinal)
                        ? moduleInfo.Ref.Substring(RefFilePrefix.Length)
                        : moduleInfo.Ref.Substring(RefFilePrefix.Length + 1);
                    long num = -1;
                    if (File.Exists(str))
                        num = new FileInfo(str).Length;
                    RaiseModuleDownloadProgressChanged(moduleInfo, 0L, num);
                    AggregateCatalog.Catalogs.Add(new AssemblyCatalog(str));
                    RaiseModuleDownloadProgressChanged(moduleInfo, num, num);
                    RecordDownloadSuccess(uri);
                    RaiseLoadModuleCompleted(moduleInfo, null);
                }
            }
            catch (Exception ex)
            {
                RaiseLoadModuleCompleted(moduleInfo, ex);
            }
        }

        private void RaiseModuleDownloadProgressChanged(ModuleInfo moduleInfo, long bytesReceived,
            long totalBytesToReceive)
        {
            RaiseModuleDownloadProgressChanged(new ModuleDownloadProgressChangedEventArgs(moduleInfo, bytesReceived,
                totalBytesToReceive));
        }

        private void RaiseModuleDownloadProgressChanged(ModuleDownloadProgressChangedEventArgs e)
            => ModuleDownloadProgressChanged?.Invoke(this, e);

        private void RaiseLoadModuleCompleted(ModuleInfo moduleInfo, Exception error)
            => RaiseLoadModuleCompleted(new LoadModuleCompletedEventArgs(moduleInfo, error));

        private void RaiseLoadModuleCompleted(LoadModuleCompletedEventArgs e) => LoadModuleCompleted?.Invoke(this, e);

        private bool IsSuccessfullyDownloaded(Uri uri)
        {
            lock (_downloadedUris)
            {
                return _downloadedUris.Contains(uri);
            }
        }

        private void RecordDownloadSuccess(Uri uri)
        {
            lock (_downloadedUris)
            {
                _downloadedUris.Add(uri);
            }
        }
    }
}