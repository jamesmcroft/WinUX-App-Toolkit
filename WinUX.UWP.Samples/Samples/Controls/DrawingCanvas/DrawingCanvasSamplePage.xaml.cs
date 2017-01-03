namespace WinUX.UWP.Samples.Samples.Controls.DrawingCanvas
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using System.Reflection;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Navigation;

    using WinUX.Input.Inking;
    using WinUX.Xaml;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DrawingCanvasSamplePage : INotifyPropertyChanged
    {
        private string eventName;

        private string pointerId;

        public DrawingCanvasSamplePage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Fired when the page has been navigated to.
        /// </summary>
        /// <param name="e">
        /// The navigation parameters.
        /// </param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (AppShell.Instance != null && AppShell.Instance.ViewModel != null)
            {
                AppShell.Instance.ViewModel.Title = "DrawingCanvas sample";
            }

            var bindingSource = this.Sample.BindingSource;

            this.Properties.Visibility = bindingSource != null
                                             ? (bindingSource.Properties.Count > 0
                                                    ? Visibility.Visible
                                                    : Visibility.Collapsed)
                                             : Visibility.Collapsed;

            if (bindingSource != null)
            {
                this.Content.DataContext = bindingSource.Bindings;
            }
        }

        private void DrawingCanvas_OnInkPointerEntering(object sender, InkPointerEventArgs args)
        {
            this.SetContent("Entering", args.PointerId.ToString());
        }

        private void DrawingCanvas_OnInkPointerExiting(object sender, InkPointerEventArgs args)
        {
            this.SetContent("Exiting", args.PointerId.ToString());
        }

        private void DrawingCanvas_OnInkPointerHovering(object sender, InkPointerEventArgs args)
        {
            this.SetContent("Hovering", args.PointerId.ToString());
        }

        private void DrawingCanvas_OnInkPointerLost(object sender, InkPointerEventArgs args)
        {
            this.SetContent("Lost", args.PointerId.ToString());
        }

        private void DrawingCanvas_OnInkPointerMoving(object sender, InkPointerEventArgs args)
        {
            this.SetContent("Moving", args.PointerId.ToString());
        }

        private void DrawingCanvas_OnInkPointerPressing(object sender, InkPointerEventArgs args)
        {
            this.SetContent("Pressing", args.PointerId.ToString());
        }

        private void DrawingCanvas_OnInkPointerReleasing(object sender, InkPointerEventArgs args)
        {
            this.SetContent("Releasing", args.PointerId.ToString());
        }

        private void SetContent(string eventFired, string associatedPointer)
        {
            UIDispatcher.Run(
                () =>
                    {
                        this.PointerId = associatedPointer;
                        this.EventName = eventFired;
                    });
        }

        public string EventName
        {
            get
            {
                return this.eventName;
            }
            set
            {
                this.Set(() => this.EventName, ref this.eventName, value);
            }
        }

        public string PointerId
        {
            get
            {
                return this.pointerId;
            }
            set
            {
                this.Set(() => this.PointerId, ref this.pointerId, value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            var myType = this.GetType();

            if (!string.IsNullOrEmpty(propertyName) && myType.GetTypeInfo().GetDeclaredProperty(propertyName) == null)
            {
                throw new ArgumentException("Property not found", propertyName);
            }
        }

        public void RaisePropertyChanging(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            var handler = this.PropertyChanging;
            handler?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        public void RaisePropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            var handler = this.PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RaisePropertyChanging<T>(Expression<Func<T>> propertyExpression)
        {
            var handler = this.PropertyChanging;
            if (handler != null)
            {
                var propertyName = GetPropertyName(propertyExpression);
                handler(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        public void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var handler = this.PropertyChanged;

            if (handler != null)
            {
                var propertyName = GetPropertyName(propertyExpression);

                if (!string.IsNullOrEmpty(propertyName))
                {
                    this.RaisePropertyChanged(propertyName);
                }
            }
        }

        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var body = propertyExpression.Body as MemberExpression;

            if (body == null)
            {
                throw new ArgumentException("Invalid argument", "propertyExpression");
            }

            var property = body.Member as PropertyInfo;

            if (property == null)
            {
                throw new ArgumentException("Argument is not a property", "propertyExpression");
            }

            return property.Name;
        }

        public bool Set<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            this.RaisePropertyChanging(propertyExpression);
            field = newValue;
            this.RaisePropertyChanged(propertyExpression);
            return true;
        }
    }
}