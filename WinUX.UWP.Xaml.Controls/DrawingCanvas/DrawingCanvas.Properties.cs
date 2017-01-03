namespace WinUX.Xaml.Controls
{
    using System.Linq;
    using System.Windows.Input;

    using Windows.Storage;
    using Windows.UI.Input.Inking;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines the properties for the <see cref="DrawingCanvas"/>.
    /// </summary>
    public sealed partial class DrawingCanvas
    {
        /// <summary>
        /// Defines the dependency property for the <see cref="DrawingAttributes"/>.
        /// </summary>
        public static readonly DependencyProperty DrawingAttributesProperty =
            DependencyProperty.Register(
                nameof(DrawingAttributes),
                typeof(InkDrawingAttributes),
                typeof(DrawingCanvas),
                new PropertyMetadata(
                    null,
                    (d, e) => ((DrawingCanvas)d).UpdateDrawingAttributes((InkDrawingAttributes)e.NewValue)));

        /// <summary>
        /// Gets or sets the attributes used for inking.
        /// </summary>
        public InkDrawingAttributes DrawingAttributes
        {
            get
            {
                return (InkDrawingAttributes)this.GetValue(DrawingAttributesProperty);
            }
            set
            {
                this.SetValue(DrawingAttributesProperty, value);
            }
        }

        /// <summary>
        /// Gets the InkCanvas control.
        /// </summary>
        public InkCanvas InkCanvas { get; private set; }

        /// <summary>
        /// Gets or sets the file of the temporary drawing.
        /// </summary>
        public StorageFile TemporaryDrawing { get; set; }

        /// <summary>
        /// Gets or sets the command called when the ink has been rendered.
        /// </summary>
        public ICommand InkRenderedCommand { get; set; }

        /// <summary>
        /// Gets or sets the command called when the ink has been changed.
        /// </summary>
        public ICommand InkChangedCommand { get; set; }

        /// <summary>
        /// Gets a value indicating whether the canvas has ink.
        /// </summary>
        public bool HasInk
            =>
            this.InkCanvas?.InkPresenter?.StrokeContainer?.GetStrokes() != null
            && this.InkCanvas.InkPresenter.StrokeContainer.GetStrokes().Any();
    }
}