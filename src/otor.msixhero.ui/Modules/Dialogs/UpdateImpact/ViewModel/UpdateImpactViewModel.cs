﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Otor.MsixHero.Appx.Updates;
using Otor.MsixHero.Infrastructure.Progress;
using Otor.MsixHero.Infrastructure.Services;
using Otor.MsixHero.Ui.Commands.RoutedCommand;
using Otor.MsixHero.Ui.Controls.ChangeableDialog.ViewModel;
using Otor.MsixHero.Ui.Controls.Progress;
using Otor.MsixHero.Ui.Domain;
using Otor.MsixHero.Ui.Helpers;
using Otor.MsixHero.Ui.Modules.Dialogs.UpdateImpact.ViewModel.Items;

namespace Otor.MsixHero.Ui.Modules.Dialogs.UpdateImpact.ViewModel
{

    public class UpdateImpactViewModel : ChangeableDialogViewModel
    {
        private readonly IAppxUpdateImpactAnalyzer updateImpactAnalyzer;
        private readonly IInteractionService interactionService;

        public UpdateImpactViewModel(IAppxUpdateImpactAnalyzer updateImpactAnalyzer, IInteractionService interactionService) : base("Analyze update impact", interactionService)
        {
            this.updateImpactAnalyzer = updateImpactAnalyzer;
            this.interactionService = interactionService;

            this.Path1 = new ChangeableFileProperty("Path to an old version", interactionService, ChangeableFileProperty.ValidatePathAndPresence)
            {
                IsValidated = true,
                Filter = "MSIX/APPX packages|*.msix;*.appx|Manifest files|AppxManifest.xml|All files|*.*"
            };

            this.Path2 = new ChangeableFileProperty("Path to a new version", interactionService, ChangeableFileProperty.ValidatePathAndPresence)
            {
                IsValidated = true,
                Filter = "MSIX/APPX packages|*.msix;*.appx|Manifest files|AppxManifest.xml|All files|*.*"
            };
            
            this.AddChildren(this.Path1, this.Path2);
            this.Compare = new DelegateCommand(this.CompareExecuted);
            this.IsValidated = false;
        }

        public ICommand Compare { get; }

        public ChangeableFileProperty Path1 { get; }

        public ChangeableFileProperty Path2 { get; }

        public ProgressProperty Progress { get; } = new ProgressProperty();

        public AsyncProperty<ComparisonViewModel> Results { get; } = new AsyncProperty<ComparisonViewModel>();

        protected override Task<bool> Save(CancellationToken cancellationToken, IProgress<ProgressData> progress)
        {
            return Task.FromResult(true);
        }

        private async void CompareExecuted(object obj)
        {
            this.IsValidated = true;

            if (!this.IsValid)
            {
                this.interactionService.ShowError(this.ValidationMessage, InteractionResult.OK, "Missing values");
                return;
            }

            this.Progress.Progress = -1;
            this.Progress.IsLoading = true;
            try
            {
                using (var cts = new CancellationTokenSource())
                {
                    var task = this.updateImpactAnalyzer.Analyze(this.Path1.CurrentValue, this.Path2.CurrentValue, cts.Token);
                    this.Progress.MonitorProgress(task, cts);
                    var result = await task.ConfigureAwait(false);

                    result.OldPackage.Path = this.Path1.CurrentValue;
                    result.NewPackage.Path = this.Path2.CurrentValue;

                    await this.Results.Load(Task.FromResult(new ComparisonViewModel(result))).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                this.interactionService.ShowError("Could not compare selected packages. " + e.Message, e);
            }
            finally
            {
                this.Progress.IsLoading = false;
            }
        }
    }
}

