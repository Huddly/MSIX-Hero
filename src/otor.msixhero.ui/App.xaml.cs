﻿using System;
using System.Windows;
using CommonServiceLocator;
using otor.msixhero.lib;
using otor.msixhero.lib.BusinessLayer.Infrastructure;
using otor.msixhero.lib.BusinessLayer.Infrastructure.Implementation;
using otor.msixhero.lib.Ipc;
using otor.msixhero.ui.Modules.Main;
using otor.msixhero.ui.Modules.Main.View;
using otor.msixhero.ui.Modules.Main.ViewModel;
using otor.msixhero.ui.Modules.PackageList;
using otor.msixhero.ui.Modules.PackageList.View;
using otor.msixhero.ui.Modules.PackageList.ViewModel;
using otor.msixhero.ui.Modules.Settings;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;

namespace otor.msixhero.ui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication, IDisposable
    {
        private readonly IProcessManager processManager = new ProcessManager();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton(typeof(IAppxPackageManager), typeof(AppxPackageManager));
            containerRegistry.RegisterSingleton(typeof(IBusyManager), typeof(BusyManager));
            containerRegistry.RegisterInstance(typeof(IProcessManager), this.processManager);
            containerRegistry.RegisterSingleton(typeof(IApplicationStateManager), typeof(ApplicationStateManager));
        }
        
        protected override Window CreateShell()
        {
            return ServiceLocator.Current.GetInstance<MainWindow>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.processManager.Dispose();
            base.OnExit(e);
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<MainModule>();
            moduleCatalog.AddModule<SettingsModule>();
            moduleCatalog.AddModule<PackageListModule>();
        }
        
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register<MainView, MainViewModel>();
            ViewModelLocationProvider.Register<PackageListView, PackageListViewModel>();
        }

        public void Dispose()
        {
            this.processManager.Dispose();
        }
    }
}
