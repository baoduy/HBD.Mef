using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;
using HBD.Framework;
using HBD.Framework.IO;
using HBD.Mef.WinForms;
using HBD.Mef.Shell.Configuration;
using HBD.Mef.Logging;
using HBD.Mef;

namespace HBD.WinForms.ModuleManagement.Plugin
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AddModuleDialog : DialogBase
    {
        [Import]
        private IShellConfigManager ShellConfigManager { get; set; }

        public AddModuleDialog()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            StatusService.SetStatus("Add new Module is opened.");
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            StatusService.SetStatus("Add new Module is closed.");
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = string.Empty;
                LoadingDialodService.ShowLoading(this, "Loading Module.");
                Enabled = false;
                backgroundWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                this.ShowErrorMessage(ex);
            }
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var step = 0;
            //1.Verify the Zip file.
            //- The zip file is existed.
            //- The zip file contains the files.
            //2. Verify file contents.
            //- At least 1 dll file should be in the zip file.
            //- The Module.json config file should be found.
            //- The Module is valid and Name is not empty.
            ZipArchive zip = null;
            ModuleConfig newModule = null;
            try
            {
                backgroundWorker.ReportProgress(++step, "Verify the zip file.");

                #region Verify the zip file

                var filePath = fileBrowserBar.SelectedPath;
                if (!File.Exists(filePath))
                {
                    backgroundWorker.ReportProgress(++step,
                        "Error: Zip file is not existed. Please check the file location.");
                    return;
                }

                try
                {
                    zip = ZipFile.OpenRead(filePath);
                }
                catch (Exception ex)
                {
                    backgroundWorker.ReportProgress(++step, $"Error: {ex.Message}");
                    Logger.Exception(ex);
                    return;
                }

                #endregion Verify the zip file

                backgroundWorker.ReportProgress(++step, "Verify the file contents.");

                #region Verify the file contents

                if (zip.Entries.Count <= 1)
                {
                    backgroundWorker.ReportProgress(++step, "Error: Zip file is empty.");
                    return;
                }

                var jsonFile = zip.Entries.FirstOrDefault(f => f.Name.StartsWith("Module", StringComparison.Ordinal)
                                                               && f.Name.EndsWith(".json", StringComparison.Ordinal));

                if (jsonFile.IsNull())
                {
                    backgroundWorker.ReportProgress(++step, "Error: The module json config file is not found.");
                    return;
                }

                var dll = zip.Entries.FirstOrDefault(f => f.Name.EndsWith(".dll", StringComparison.Ordinal));

                if (dll.IsNull())
                {
                    backgroundWorker.ReportProgress(++step, "Error: The module doesn't have any binary file.");
                    return;
                }

                #endregion Verify the file contents

                backgroundWorker.ReportProgress(++step, "Verify the config file.");

                #region Verify the config file

                try
                {
                    // ReSharper disable once PossibleNullReferenceException
                    var tmpFileName = Path.Combine(Path.GetTempPath(), jsonFile.Name);
                    jsonFile.ExtractToFile(tmpFileName, true);
                    newModule = JsonConfigHelper.ReadConfig<ModuleConfig>(tmpFileName);
                    File.Delete(tmpFileName);

                    if (newModule.Name.IsNullOrEmpty())
                    {
                        backgroundWorker.ReportProgress(++step, "Error: Module Name is empty.");
                        return;
                    }

                    if (newModule.Version.IsNullOrEmpty())
                    {
                        backgroundWorker.ReportProgress(++step, "Error: Module Version is empty.");
                        return;
                    }
                    //Verify the binaries if have
                    if (newModule.AssemplyFiles.Count > 0)
                    {
                        var notfounds =
                            newModule.AssemplyFiles.Where(a => !zip.Entries.Any(t => t.Name.EndsWith(a))).ToList();
                        if (notfounds.Any())
                        {
                            backgroundWorker.ReportProgress(++step,
                                $"Error: Binaries are not found '{string.Join(",", notfounds)}'.");
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    backgroundWorker.ReportProgress(++step, $"Error: {ex.Message}");
                    Logger.Exception(ex);
                    return;
                }

                #endregion Verify the config file

                //Check duplicate module. if existing module then update the existing one.
                backgroundWorker.ReportProgress(++step, "Check Module duplicating.");

                #region Check Module duplicating

                var oldModule = ShellConfigManager.Modules.FirstOrDefault(m => m.Name.EqualsIgnoreCase(newModule.Name));
                if (oldModule != null)
                {
                    backgroundWorker.ReportProgress(++step,
                        $"The module '{oldModule.Name}' is an existing module with version '{oldModule.Version}'.");

                    //Compare the version on both Modules.
                    if (
                        this.ShowConfirmationMessage(
                            $"Do you want to overwrite module:\n- '{oldModule.Name}' version '{oldModule.Version}' with version '{newModule.Version}'?") !=
                        DialogResult.Yes)
                    {
                        backgroundWorker.ReportProgress(++step, $"The module importing had been canceled by user.");
                        return;
                    }

                    if (oldModule.IsEnabled)
                    {
                        backgroundWorker.ReportProgress(++step,
                            $"The old module '{oldModule.Name}' cannot be overwritten because it is enabled. Please disable it and restart the application in order to overwrite the module '{newModule.Name}'.");
                        return;
                    }

                    if (!oldModule.AllowToManage)
                    {
                        backgroundWorker.ReportProgress(++step,
                            $"The old module '{oldModule.Name}' is not allow to be overwritten as allow to manage is disabled.");
                        return;
                    }

                    Directory.CreateDirectory(ShellConfigManager.ShellConfig.BackupModulePath);
                    var backupFileName = Path.Combine(ShellConfigManager.ShellConfig.BackupModulePath,
                        new DirectoryInfo(oldModule.Directory).Name + "_" + DateTime.Now.ToString("yyyy.mm.dd-hh.mm.ss") +
                        ".zip");

                    ZipFile.CreateFromDirectory(oldModule.Directory, backupFileName, CompressionLevel.Optimal, false);
                    backgroundWorker.ReportProgress(++step,
                        $"Backup the old module '{oldModule.Name}' to {backupFileName}.");

                    Directory.Delete(oldModule.Directory, true);
                }

                #endregion Check Module duplicating

                backgroundWorker.ReportProgress(++step, "Extract zip file to Module folder.");

                #region Extract zip file to Module folder.

                // ReSharper disable once AssignNullToNotNullAttribute
                var extractFolderName = Path.Combine(ShellConfigManager.ShellConfig.ModulePath,
                    Path.GetFileNameWithoutExtension(filePath));
                zip.ExtractToDirectory(extractFolderName);
                //Check if the extracted folder had only 1 subfolder and the same is exactly the same with parent folder.
                //Then move all files and folders to the parent then delete subfolder.
                var extractDirectory = new DirectoryInfo(extractFolderName);
                if (extractDirectory.GetDirectories().Length == 1)
                {
                    var sub = extractDirectory.GetDirectories().First();
                    if (sub.Name.EqualsIgnoreCase(extractDirectory.Name))
                        sub.MoveAllFilesAndFoldersTo(extractDirectory.FullName);
                }

                #endregion Extract zip file to Module folder.

                backgroundWorker.ReportProgress(++step,
                    "New module had been imported. Please restart the application to use the new module.");
            }
            catch (Exception ex)
            {
                backgroundWorker.ReportProgress(++step, $"Error: {ex.Message}");
                backgroundWorker.ReportProgress(++step,
                    "Import module failed. Please re-start the application and try again.");
                Logger.Exception(ex);
            }
            finally
            {
                //Close the zip file
                zip?.Dispose();
                //Write info to log.
                Logger.Info(textBox1.Text);
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadingDialodService.HideLoading(this);
            //TODO: Show dialog here.
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            textBox1.AppendText($"{e.ProgressPercentage}. {e.UserState} {Environment.NewLine}");
            LoadingDialodService.ShowLoading(this, $"{e.ProgressPercentage} {e.UserState}");

            Enabled = true;
            btn_Add.Enabled = false;
            Focus();
        }

        private void fileBrowserBar_Change(object sender, EventArgs e)
        {
            btn_Add.Enabled = fileBrowserBar.SelectedPath.IsNotNull();
        }
    }
}