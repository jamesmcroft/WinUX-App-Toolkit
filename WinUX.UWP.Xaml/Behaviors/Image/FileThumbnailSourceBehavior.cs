namespace WinUX.Xaml.Behaviors.Image
{
    using System;

    using Windows.Storage;
    using Windows.Storage.FileProperties;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Imaging;

    using Microsoft.Xaml.Interactivity;

    /// <summary>
    /// Defines a behavior for setting the source of an Image based on a <see cref="StorageFile"/> thumbnail.
    /// </summary>
    public sealed class FileThumbnailSourceBehavior : Behavior
    {
        /// <summary>
        /// Defines the dependency property for <see cref="File"/>.
        /// </summary>
        public static readonly DependencyProperty FileProperty = DependencyProperty.Register(
            nameof(File),
            typeof(StorageFile),
            typeof(FileThumbnailSourceBehavior),
            new PropertyMetadata(
                null,
                (d, e) => ((FileThumbnailSourceBehavior)d).OnFileChanged((StorageFile)e.NewValue)));

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        public StorageFile File
        {
            get
            {
                return (StorageFile)this.GetValue(FileProperty);
            }
            set
            {
                this.SetValue(FileProperty, value);
            }
        }

        private Image Image => this.AssociatedObject as Image;

        private async void OnFileChanged(StorageFile newFile)
        {
            if (this.Image == null || newFile == null)
            {
                return;
            }

            var thumb = await newFile.GetThumbnailAsync(ThumbnailMode.SingleItem, 32, ThumbnailOptions.ResizeThumbnail);

            if (thumb == null)
            {
                return;
            }

            var bitmapImage = new BitmapImage();
            bitmapImage.SetSource(thumb.CloneStream());

            this.Image.Source = bitmapImage;
        }
    }
}