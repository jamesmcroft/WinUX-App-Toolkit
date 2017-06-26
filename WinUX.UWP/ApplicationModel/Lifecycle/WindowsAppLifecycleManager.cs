namespace WinUX.ApplicationModel.Lifecycle
{
    using System.Threading.Tasks;

    using Windows.ApplicationModel;

    /// <summary>
    /// Defines a provider for managing the suspend and resume lifecycle events of an application.
    /// </summary>
    public class WindowsAppLifecycleManager : AppLifecycleManager
    {
        /// <inheritdoc />
        public override async Task SuspendAsync(object suspensionArgs)
        {
            var args = suspensionArgs as SuspendingEventArgs;
            if (args != null)
            {
                var suspendingDeferral = args.SuspendingOperation.GetDeferral();

                await base.SuspendAsync(suspensionArgs);

                suspendingDeferral.Complete();
            }
        }
    }
}