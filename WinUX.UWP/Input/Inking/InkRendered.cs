namespace WinUX.UWP.Input.Inking
{
    using System;

    using Windows.Storage;

    /// <summary>
    /// Handler for when ink has finished rendering.
    /// </summary>
    /// <param name="sender">
    /// The originater.
    /// </param>
    /// <param name="args">
    /// The ink rendered arguments containing the rendered file.
    /// </param>
    public delegate void InkRenderedEventHandler(object sender, InkRenderedEventArgs args);

    /// <summary>
    /// Defines event arguments for when ink has rendered to a <see cref="StorageFile"/>.
    /// </summary>
    public sealed class InkRenderedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InkRenderedEventArgs"/> class.
        /// </summary>
        /// <param name="renderedFile">
        /// The rendered file.
        /// </param>
        public InkRenderedEventArgs(StorageFile renderedFile)
        {
            this.RenderedFile = renderedFile;
        }

        /// <summary>
        /// Gets the rendered file.
        /// </summary>
        public StorageFile RenderedFile { get; private set; }
    }
}