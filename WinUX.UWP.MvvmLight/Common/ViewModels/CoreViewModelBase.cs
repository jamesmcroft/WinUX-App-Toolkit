namespace WinUX.MvvmLight.Common.ViewModels
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Windows.ApplicationModel.Core;
    using Windows.System.Profile;
    using Windows.UI.Core;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Messaging;

    using WinUX.Application.ViewManagement;
    using WinUX.ApplicationModel.Lifecycle;
    using WinUX.Device.Profile;
    using WinUX.Exceptions;
    using WinUX.Messaging.Dialogs;
    using WinUX.Mvvm.Services;

    /// <summary>
    /// Defines a core base view model which contains all of the core providers that may be required.
    /// </summary>
    public abstract class CoreViewModelBase : ViewModelBase, ISuspendable
    {
        /// <summary>
        /// Gets a unique identifier for the view model instance.
        /// </summary>
        public readonly Guid InstanceId = Guid.NewGuid();

        private bool isSecondaryView;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreViewModelBase"/> class.
        /// </summary>
        /// <param name="messenger">
        /// The MvvmLight IMessenger.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if a parameter is null.
        /// </exception>
        protected CoreViewModelBase(IMessenger messenger)
        {
            if (messenger == null)
            {
                throw new ArgumentNullException(nameof(messenger));
            }

            this.MessengerInstance = messenger;

            this.SyncContext = SynchronizationContext.Current;
            this.RegisterWithView();
        }

        /// <summary>
        /// Gets a value indicating whether this view model is for a secondary view.
        /// </summary>
        public bool IsSecondaryView
        {
            get
            {
                return this.isSecondaryView;
            }
            set
            {
                this.isSecondaryView = value;
                if (value)
                {
                    this.RegisterWithView();
                }
            }
        }

        /// <summary>
        /// Gets the identifier for the application view this view model was created for.
        /// </summary>
        public int ViewId { get; private set; }

        /// <summary>
        /// Gets the current device type the application is running on.
        /// </summary>
        public DeviceType CurrentDeviceType => AnalyticsInfo.VersionInfo.GetDeviceType();

        /// <summary>
        /// Gets the <see cref="CoreDispatcher"/> for the view that created this view model.
        /// </summary>
        public CoreDispatcher UIDispatcher => ViewCoreDispatcherManager.Current.Get(this.ViewId);

        /// <summary>
        /// Gets the synchronization context for the view model.
        /// </summary>
        public SynchronizationContext SyncContext { get; }

        /// <summary>
        /// Gets the <see cref="INavigationService"/> for the view that created this view model.
        /// </summary>
        public INavigationService NavigationService
            =>
            this.IsSecondaryView
                ? ViewNavigationServiceManager.Current.Get(this.ViewId)
                : Mvvm.Services.NavigationService.Current;

        /// <summary>
        /// Gets the <see cref="MessageDialogManager"/> for the view that created this view model.
        /// </summary>
        public MessageDialogManager Dialog
            => this.IsSecondaryView ? ViewMessageDialogManager.Current.Get(this.ViewId) : MessageDialogManager.Current;

        /// <inheritdoc />
        public override void RaisePropertyChanged(string propertyName = null)
        {
            try
            {
                this.SyncContext.Post(
                    state =>
                        {
                            try
                            {
                                base.RaisePropertyChanged(propertyName);
                            }
                            catch (Exception ex)
                            {
#if DEBUG
                                System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
                            }
                        },
                    null);
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
            }
        }

        private void RegisterWithView()
        {
            try
            {
                // Check if we have a ViewId and update if required.
                if (this.ViewId == 0 || this.ViewId == -1)
                {
                    var view = ApplicationView.GetForCurrentView();
                    this.ViewId = view?.Id ?? -1;
                }

                if (this.IsSecondaryView)
                {
                    if (this.NavigationService == null)
                    {
                        var viewFrame = Window.Current.Content as Frame;
                        if (viewFrame == null)
                        {
                            throw new UnexpectedNullPropertyException("Window.Current.Content as Frame", typeof(Frame));
                        }

                        ViewNavigationServiceManager.Current.RegisterOrUpdate(
                            this.ViewId,
                            new NavigationService(viewFrame));
                    }

                    var viewDispatcher = CoreApplication.GetCurrentView().Dispatcher;
                    if (viewDispatcher == null)
                    {
                        throw new UnexpectedNullPropertyException(
                                  "CoreApplication.GetCurrentView().Dispatcher",
                                  typeof(CoreDispatcher));
                    }

                    if (this.UIDispatcher == null)
                    {
                        ViewCoreDispatcherManager.Current.RegisterOrUpdate(this.ViewId, viewDispatcher);
                    }

                    if (this.Dialog == null)
                    {
                        ViewMessageDialogManager.Current.RegisterOrUpdate(
                            this.ViewId,
                            new MessageDialogManager(viewDispatcher));
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
            }
        }

        /// <inheritdoc />
        public Task SuspendAsync()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task ResumeAsync()
        {
            return Task.CompletedTask;
        }
    }
}