﻿using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Ookii.Dialogs.Wpf;
using otor.msixhero.lib.Infrastructure;
using Application = System.Windows.Application;

namespace otor.msixhero.ui.Services
{
    public class InteractionService : IInteractionService
    {
        private readonly SynchronizationContext context;

        public InteractionService()
        {
            this.context = SynchronizationContext.Current;
        }

        public InteractionResult Confirm(string body, string title = null, InteractionType type = InteractionType.Asterisk, InteractionButton buttons = InteractionButton.OK)
        {
            var targetType = (MessageBoxIcon)(int)type;
            var targetButtons = (MessageBoxButtons)(int)buttons;

            var result = MessageBox.Show(body, title, targetButtons, targetType);
            return (InteractionResult)(int)result;
        }

        public InteractionResult ShowError(string body, string title = null, string extendedInfo = null)
        {
            var taskDialog = new TaskDialog
            {
                MainIcon = TaskDialogIcon.Error, 
                ButtonStyle = TaskDialogButtonStyle.Standard
            };

            taskDialog.Buttons.Add(new TaskDialogButton(ButtonType.Retry)); 
            taskDialog.Buttons.Add(new TaskDialogButton(ButtonType.Close));

            taskDialog.CenterParent = true;
            taskDialog.Content = body;

            if (!string.IsNullOrEmpty(extendedInfo))
            {
                taskDialog.ExpandedInformation = extendedInfo;
            }

            taskDialog.WindowTitle = title ?? "MSIX Hero - Error";

            if (this.context == null)
            {
                return (InteractionResult)(int)taskDialog.ShowDialog(Application.Current.MainWindow).ButtonType;
            }

            var result = 0;
            this.context.Send(state => result = (int)taskDialog.ShowDialog(Application.Current.MainWindow).ButtonType, null);
            return (InteractionResult) result;
        }

        public bool SelectFile(string initialFile, string filterString, out string selectedFile)
        {
            if (!SelectFile(initialFile, filterString, false, out var selection))
            {
                selectedFile = null;
                return false;
            }

            selectedFile = selection.FirstOrDefault();
            return true;
        }

        public bool SaveFile(string initialFile, string filterString, out string selectedFile)
        {
            var dlg = new SaveFileDialog();

            if (!string.IsNullOrEmpty(filterString))
            {
                dlg.Filter = filterString;
            }

            if (initialFile != null)
            {
                var fileInfo = new FileInfo(initialFile);
                if (fileInfo.Directory != null && fileInfo.Directory.Exists)
                {
                    dlg.InitialDirectory = fileInfo.Directory.FullName;
                }

                dlg.FileName = fileInfo.Name;
            }

            dlg.CheckFileExists = false;
            var result = dlg.ShowDialog() == DialogResult.OK;
            selectedFile = dlg.FileName;
            return result;
        }

        public bool SaveFile(string filterString, out string selectedFile)
        {
            return this.SaveFile(null, filterString, out selectedFile);
        }

        public bool SaveFile(out string selectedFile)
        {
            return this.SaveFile(null, null, out selectedFile);
        }

        public bool SelectFile(string filterString, out string selectedFile)
        {
            return this.SelectFile(null, filterString, out selectedFile);
        }

        public bool SelectFile(out string selectedFile)
        {
            return this.SelectFile(null, "*.*|All files (*.*)", out selectedFile);
        }

        public bool SelectFiles(string initialFile, string filterString, out string[] selectedFiles)
        {
            return SelectFile(initialFile, filterString, true, out selectedFiles);
        }

        public bool SelectFiles(string filterString, out string[] selectedFiles)
        {
            return SelectFile(null, filterString, true, out selectedFiles);
        }

        public bool SelectFolder(string initialFolder, out string selectedFolder)
        {
            var dlg = new VistaFolderBrowserDialog
            {
                SelectedPath = initialFolder
            };

            var result = dlg.ShowDialog() == true;
            selectedFolder = dlg.SelectedPath;
            return result;
        }

        public bool SelectFolder(out string selectedFolder)
        {
            return this.SelectFolder(null, out selectedFolder);
        }

        private static bool SelectFile(string initialFile, string filterString, bool withMultiSelection, out string[] selectedFiles)
        {
            var dlg = new OpenFileDialog();

            if (!string.IsNullOrEmpty(filterString))
            {
                dlg.Filter = filterString;
            }

            if (initialFile != null)
            {
                var fileInfo = new FileInfo(initialFile);
                if (fileInfo.Directory != null && fileInfo.Directory.Exists)
                {
                    dlg.InitialDirectory = fileInfo.Directory.FullName;
                }

                dlg.FileName = fileInfo.Name;
            }

            dlg.CheckFileExists = true;
            dlg.Multiselect = withMultiSelection;

            var result = dlg.ShowDialog() == DialogResult.OK;
            selectedFiles = dlg.FileNames;
            return result;
        }
    }
}
