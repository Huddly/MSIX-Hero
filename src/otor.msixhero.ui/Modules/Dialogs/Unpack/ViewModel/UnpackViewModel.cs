﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Otor.MsixHero.Appx.Packaging.Packer;
using Otor.MsixHero.Infrastructure.Progress;
using Otor.MsixHero.Infrastructure.Services;
using Otor.MsixHero.Ui.Commands.RoutedCommand;
using Otor.MsixHero.Ui.Controls.ChangeableDialog.ViewModel;
using Otor.MsixHero.Ui.Domain;

namespace Otor.MsixHero.Ui.Modules.Dialogs.Unpack.ViewModel
{
    public class UnpackViewModel : ChangeableDialogViewModel
    {
        private readonly IAppxPacker appxPacker;
        private ICommand openSuccessLink;
        private ICommand reset;

        public UnpackViewModel(IAppxPacker appxPacker, IInteractionService interactionService) : base("Unpack MSIX package", interactionService)
        {
            this.appxPacker = appxPacker;

            this.OutputPath = new ChangeableFolderProperty(interactionService)
            {
                DisplayName = "Target directory",
                Validators = new[] { ChangeableFolderProperty.ValidatePath }
            };

            this.InputPath = new ChangeableFileProperty(interactionService)
            {
                DisplayName = "Source package path",
                Validators = new[] { ChangeableFileProperty.ValidatePath },
                Filter = "MSIX/APPX packages|*.msix;*.appx|All files|*.*"
            };
            
            this.CreateFolder = new ChangeableProperty<bool>(true);

            this.InputPath.ValueChanged += this.InputPathOnValueChanged;

            this.AddChildren(this.InputPath, this.OutputPath, this.CreateFolder);
        }
        
        private void InputPathOnValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.OutputPath.CurrentValue))
            {
                return;
            }

            var newFilePath = new FileInfo((string) e.NewValue);

            // ReSharper disable once PossibleNullReferenceException
            var directory = newFilePath.Directory.FullName;

            var fileName = newFilePath.Name;

            this.OutputPath.CurrentValue = Path.Join(directory, fileName + "_extracted");
        }

        public ChangeableFolderProperty OutputPath { get; }

        public ChangeableFileProperty InputPath { get; }
        public ICommand OpenSuccessLinkCommand
        {
            get { return this.openSuccessLink ??= new DelegateCommand(this.OpenSuccessLinkExecuted); }
        }

        public ICommand ResetCommand
        {
            get { return this.reset ??= new DelegateCommand(this.ResetExecuted); }
        }

        public ChangeableProperty<bool> CreateFolder { get; }

        protected override async Task<bool> Save(CancellationToken cancellationToken, IProgress<ProgressData> progress)
        {
            await this.appxPacker.Unpack(this.InputPath.CurrentValue, this.GetOutputPath(), default, progress).ConfigureAwait(false);
            return true;
        }

        private void ResetExecuted(object parameter)
        {
            this.InputPath.Reset();
            this.OutputPath.Reset();
            this.State.IsSaved = false;
        }

        private void OpenSuccessLinkExecuted(object parameter)
        { 
            Process.Start("explorer.exe", this.GetOutputPath());
        }

        private string GetOutputPath()
        {
            if (!string.IsNullOrEmpty(this.InputPath.CurrentValue))
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                return this.CreateFolder.CurrentValue ? Path.Combine(this.OutputPath.CurrentValue, Path.GetFileNameWithoutExtension(this.InputPath.CurrentValue)) : this.OutputPath.CurrentValue;
            }

            return null;
        }
    }
}

