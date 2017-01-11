namespace WinUX.Threading
{
    using System;
    using System.Threading;

    /// <summary>
    /// Defines a helper for using the <see cref="SynchronizationContext"/>.
    /// </summary>
    public static class SynchronizationContextHelper
    {
        private static SynchronizationContext context;

        private static SynchronizationContext Context
            => context ?? (context = SynchronizationContext.Current ?? new SynchronizationContext());

        /// <summary>
        /// Invokes an event handler with the <see cref="SynchronizationContext"/>.
        /// </summary>
        /// <typeparam name="TEventArgs">
        /// The type of event arguments.
        /// </typeparam>
        /// <param name="handler">
        /// The handler to invoke.
        /// </param>
        /// <param name="sender">
        /// The originating sender.
        /// </param>
        /// <param name="args">
        /// The handler event arguments.
        /// </param>
        public static void Execute<TEventArgs>(Delegate handler, object sender, TEventArgs args)
        {
            Context.Post(delegate { handler?.DynamicInvoke(sender, args); }, null);
        }
    }
}