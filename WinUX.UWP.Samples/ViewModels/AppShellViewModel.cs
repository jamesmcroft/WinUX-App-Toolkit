namespace WinUX.UWP.Samples.ViewModels
{
    using System.Collections.ObjectModel;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using GalaSoft.MvvmLight.Messaging;

    using WinUX.Xaml.Controls;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using WinUX.Diagnostics.Tracing;
    using WinUX.MvvmLight.Common.ViewModels;
    using WinUX.UWP.Samples.Components;

    public sealed class AppShellViewModel : CoreViewModelBase
    {
        private bool isAppShellInitialized;

        private bool isAppPaneEnabled = true;

        private bool isFullscreen;

        private string title;

        private readonly SampleService sampleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppShellViewModel"/> class.
        /// </summary>
        /// <param name="sampleService">
        /// The sample service.
        /// </param>
        /// <param name="messenger">
        /// The messenger.
        /// </param>
        public AppShellViewModel(SampleService sampleService, IMessenger messenger)
            : base(messenger)
        {
            if (sampleService == null)
            {
                throw new ArgumentNullException(nameof(sampleService));
            }

            this.sampleService = sampleService;

            this.PrimaryAppButtons = new ObservableCollection<AppMenuButton>();
            this.SecondaryAppButtons = new ObservableCollection<AppMenuButton>();
            this.FlyoutButtons = new ObservableCollection<FlyoutAppMenuButton>();
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.title) ? "WinUX Samples" : this.title;
            }
            set
            {
                this.Set(() => this.Title, ref this.title, value);
            }
        }

        /// <summary>
        /// Gets the primary app buttons.
        /// </summary>
        public ObservableCollection<AppMenuButton> PrimaryAppButtons { get; }

        /// <summary>
        /// Gets the secondary app buttons.
        /// </summary>
        public ObservableCollection<AppMenuButton> SecondaryAppButtons { get; }

        /// <summary>
        /// Gets the fly-out app buttons.
        /// </summary>
        public ObservableCollection<FlyoutAppMenuButton> FlyoutButtons { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the app pane is enabled.
        /// </summary>
        public bool IsAppPaneEnabled
        {
            get
            {
                return this.isAppPaneEnabled;
            }
            set
            {
                this.Set(() => this.IsAppPaneEnabled, ref this.isAppPaneEnabled, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the app frame is full-screen.
        /// </summary>
        public bool IsFullscreen
        {
            get
            {
                return this.isFullscreen;
            }
            set
            {
                this.Set(() => this.IsFullscreen, ref this.isFullscreen, value);
            }
        }

        /// <summary>
        /// Initializes the application shell.
        /// </summary>
        public async Task InitializeAsync()
        {
            if (this.isAppShellInitialized)
            {
                return;
            }

            this.isAppShellInitialized = true;

            try
            {
                await this.sampleService.InitializeAsync();
            }
            catch (Exception ex)
            {
                EventLogger.Current.WriteError(ex.Message);
            }

            this.Title = "WinUX Samples";

            this.PrimaryAppButtons.Clear();
            this.SecondaryAppButtons.Clear();
            this.FlyoutButtons.Clear();

            this.PrimaryAppButtons.Add(CreateHomeButton());
            this.PrimaryAppButtons.AddRange(this.CreateSampleCollectionButtons());
        }

        private IEnumerable<AppMenuButton> CreateSampleCollectionButtons()
        {
            var sampleButtons = new List<AppMenuButton>();

            if (this.sampleService.SampleCollections != null)
            {
                foreach (var collection in this.sampleService.SampleCollections)
                {
                    if (string.IsNullOrWhiteSpace(collection.SourcePageType))
                    {
                        continue;
                    }

                    AppMenuButton sampleButton;

                    try
                    {
                        sampleButton = new AppMenuButton
                                           {
                                               Page = Type.GetType(collection.SourcePageType),
                                               IsGrouped = true,
                                               PageParameter = collection
                                           };

                        if (!string.IsNullOrWhiteSpace(collection.IconPathData))
                        {
                            sampleButton.Content = GenerateButtonContentWithPath(
                                collection.IconPathData,
                                collection.Name);
                        }
                        else
                        {
                            sampleButton.Content = GenerateButtonContentWithSymbol(
                                collection.IconSymbol,
                                collection.Name);
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    sampleButtons.Add(sampleButton);
                }
            }

            return sampleButtons;
        }

        private static AppMenuButton CreateHomeButton()
        {
            var homeButton = new AppMenuButton
                                 {
                                     ClearNavigationStack = true,
                                     Page = typeof(MainPage),
                                     Content = GenerateButtonContentWithSymbol(Symbol.Home, "Home"),
                                     ToolTip = "Navigate to home",
                                     IsGrouped = true
                                 };

            return homeButton;
        }

        private static UIElement GenerateButtonContentWithSymbol(Symbol icon, string labelText)
        {
            var symbolIcon = new SymbolIcon(icon) { Width = 48, Height = 48 };
            return GenerateButtonContent(symbolIcon, labelText);
        }

        private static UIElement GenerateButtonContentWithPath(string pathData, string labelText)
        {
            var pathIcon = pathData.ToPathIcon();
            return GenerateButtonContent(pathIcon, labelText);
        }

        private static UIElement GenerateButtonContent(UIElement icon, string labelText)
        {
            var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };

            var label = new TextBlock
                            {
                                Margin = new Thickness(12, 0, 0, 0),
                                VerticalAlignment = VerticalAlignment.Center,
                                Text = labelText
                            };

            stackPanel.Children.Add(icon);
            stackPanel.Children.Add(label);

            return stackPanel;
        }
    }
}