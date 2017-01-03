namespace WinUX.UWP.Samples.ViewModels
{
    using System;

    using GalaSoft.MvvmLight.Ioc;

    using Microsoft.Practices.ServiceLocation;

    using WinUX.MvvmLight.Xaml.Views;

    /// <summary>
    /// Defines a base view model for the <see cref="PageBase"/> XAML component.
    /// </summary>
    public abstract class SamplePageBaseViewModel : PageBaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SamplePageBaseViewModel"/> class.
        /// </summary>
        protected SamplePageBaseViewModel()
            : this(ServiceLocator.Current.GetInstance<AppShellViewModel>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SamplePageBaseViewModel"/> class.
        /// </summary>
        /// <param name="appShellVm">
        /// The application shell view model.
        /// </param>
        [PreferredConstructor]
        internal SamplePageBaseViewModel(AppShellViewModel appShellVm)
        {
            if (appShellVm == null)
            {
                throw new ArgumentNullException(nameof(appShellVm));
            }

            this.AppShell = appShellVm;
        }

        /// <summary>
        /// Gets the application shell.
        /// </summary>
        public AppShellViewModel AppShell { get; }
    }
}