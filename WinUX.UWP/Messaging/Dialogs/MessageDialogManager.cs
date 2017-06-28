namespace WinUX.Messaging.Dialogs
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Windows.UI.Core;
    using Windows.UI.Popups;

    using WinUX.Application.ViewManagement;
    using WinUX.Xaml;

    /// <summary>
    /// Defines a provider for handling the <see cref="MessageDialog"/>.
    /// </summary>
    public class MessageDialogManager : IDisposable
    {
        private static MessageDialogManager current;

        private readonly CoreDispatcher dispatcher;

        private SemaphoreSlim semaphore = new SemaphoreSlim(1);

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageDialogManager"/> class.
        /// </summary>
        /// <param name="dispatcher">
        /// The core dispatcher used for running message dialog logic.
        /// </param>
        public MessageDialogManager(CoreDispatcher dispatcher)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException(
                          nameof(dispatcher),
                          "If you're using MessageDialogManager.Current, remember to initialize the UIDispatcher from the OnLaunched event in your App.xaml.cs.");
            }

            this.dispatcher = dispatcher;
        }

        /// <summary>
        /// Gets an instance of the <see cref="MessageDialogManager"/>.
        /// </summary>
        /// <remarks>
        /// Only use current in conjunction with the <see cref="UIDispatcher"/>.
        /// It is not intended to be used with <see cref="ViewMessageDialogManager"/>.
        /// </remarks>
        public static MessageDialogManager Current
            => current ?? (current = new MessageDialogManager(UIDispatcher.Instance));

        /// <summary>
        /// Shows the message dialog with a given message.
        /// </summary>
        /// <param name="message">
        /// The message to show.
        /// </param>
        public void Show(string message)
        {
            this.ShowAsync(message).ConfigureAwait(false);
        }

        /// <summary>
        /// Shows the message dialog with a given title and message.
        /// </summary>
        /// <param name="title">
        /// The title of the dialog.
        /// </param>
        /// <param name="message">
        /// The message to show.
        /// </param>
        public void Show(string title, string message)
        {
            this.ShowAsync(title, message).ConfigureAwait(false);
        }

        /// <summary>
        /// Shows the message dialog with a given message and buttons.
        /// </summary>
        /// <param name="message">
        /// The message to show.
        /// </param>
        /// <param name="commands">
        /// The buttons to show with corresponding commands.
        /// </param>
        public void Show(string message, params IUICommand[] commands)
        {
            this.ShowAsync(message, commands).ConfigureAwait(false);
        }

        /// <summary>
        /// Shows the message dialog with a given title, message and buttons.
        /// </summary>
        /// <param name="title">
        /// The title of the dialog.
        /// </param>
        /// <param name="message">
        /// The message to show.
        /// </param>
        /// <param name="commands">
        /// The buttons to show with corresponding commands.
        /// </param>
        public void Show(string title, string message, params IUICommand[] commands)
        {
            this.ShowAsync(title, message, commands).ConfigureAwait(false);
        }

        /// <summary>
        /// Shows the message dialog with a given message asynchronously.
        /// </summary>
        /// <param name="message">
        /// The message to show.
        /// </param>
        /// <returns>
        /// Returns an awaitable task.
        /// </returns>
        public async Task ShowAsync(string message)
        {
            await this.ShowAsync(null, message, null);
        }

        /// <summary>
        /// Shows the message dialog with a given title and message asynchronously.
        /// </summary>
        /// <param name="title">
        /// The title of the dialog.
        /// </param>
        /// <param name="message">
        /// The message to show.
        /// </param>
        /// <returns>
        /// Returns an awaitable task.
        /// </returns>
        public async Task ShowAsync(string title, string message)
        {
            await this.ShowAsync(title, message, null);
        }

        /// <summary>
        /// Shows the message dialog with a given message and buttons asynchronously.
        /// </summary>
        /// <param name="message">
        /// The message to show.
        /// </param>
        /// <param name="commands">
        /// The buttons to show with corresponding commands.
        /// </param>
        /// <returns>
        /// Returns an awaitable task.
        /// </returns>
        public async Task ShowAsync(string message, params IUICommand[] commands)
        {
            await this.ShowAsync(null, message, commands);
        }

        /// <summary>
        /// Shows the message dialog with a given title, message and buttons asynchronously.
        /// </summary>
        /// <param name="title">
        /// The title of the dialog.
        /// </param>
        /// <param name="message">
        /// The message to show.
        /// </param>
        /// <param name="commands">
        /// The buttons to show with corresponding commands.
        /// </param>
        /// <returns>
        /// Returns an awaitable task.
        /// </returns>
        public async Task ShowAsync(string title, string message, params IUICommand[] commands)
        {
            var tcs = new TaskCompletionSource<bool>();

            try
            {
                await this.dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    async () =>
                        {
                            await this.semaphore.WaitAsync();

                            try
                            {
                                var dialog = new MessageDialog(message)
                                                 {
                                                     Title =
                                                         string.IsNullOrWhiteSpace(title)
                                                             ? "Message"
                                                             : title
                                                 };

                                if (commands != null)
                                {
                                    foreach (var command in commands)
                                    {
                                        dialog.Commands.Add(command);
                                    }
                                }

                                await dialog.ShowAsync();
                            }
                            catch (Exception ex)
                            {
#if DEBUG
                                System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
                            }
                            finally
                            {
                                this.semaphore.Release();
                            }
                            tcs.SetResult(true);
                        });

                await tcs.Task;
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || this.semaphore == null)
            {
                return;
            }

            this.semaphore.Dispose();
            this.semaphore = null;
        }
    }
}