namespace WinUX.UWP.ApplicationModel.Lifecycle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Windows.ApplicationModel;

    /// <summary>
    /// Defines a provider for managing the suspend and resume lifecycle events of an application.
    /// </summary>
    public class AppLifecycleManager : IAppLifecycleManager
    {
        private static AppLifecycleManager current;

        private readonly List<WeakReference<ISuspendable>> items;

        /// <summary>
        /// Gets an instance of the <see cref="AppLifecycleManager"/>.
        /// </summary>
        public static AppLifecycleManager Current => current ?? (current = new AppLifecycleManager());

        /// <summary>
        /// Initializes a new instance of the <see cref="AppLifecycleManager"/> class.
        /// </summary>
        public AppLifecycleManager()
        {
            this.items = new List<WeakReference<ISuspendable>>();
        }

        /// <inheritdoc />
        public void Add(ISuspendable suspendable)
        {
            if (suspendable == null)
            {
                throw new ArgumentNullException(nameof(suspendable));
            }

            this.items.Add(new WeakReference<ISuspendable>(suspendable));
        }

        /// <inheritdoc />
        public void Remove(ISuspendable suspendable)
        {
            if (suspendable == null)
            {
                throw new ArgumentNullException(nameof(suspendable));
            }

            var existingItem = this.items.FirstOrDefault(
                x =>
                    {
                        ISuspendable s;
                        var exists = x.TryGetTarget(out s);

                        if (exists)
                        {
                            return s == suspendable;
                        }

                        return false;
                    });

            this.items.Remove(existingItem);
        }

        /// <inheritdoc />
        public async Task SuspendAsync(SuspendingEventArgs args)
        {
            var suspendingDeferral = args.SuspendingOperation.GetDeferral();

            var suspendables = new List<WeakReference<ISuspendable>>();
            var suspendTasks = new List<Task>();

            foreach (var item in this.items)
            {
                ISuspendable s;
                var targetExists = item.TryGetTarget(out s);
                if (targetExists)
                {
                    suspendables.Add(item);

                    var suspendTask = s.SuspendAsync();
                    if (suspendTask == null)
                    {
                        continue;
                    }

                    if (!suspendTask.IsFaulted)
                    {
                        suspendTasks.Add(suspendTask);
                    }
                    else
                    {
                        suspendables.Remove(item);
                    }
                }
                else
                {
                    suspendables.Remove(item);
                }
            }

            await Task.WhenAll(suspendTasks);

            suspendingDeferral.Complete();
        }

        /// <inheritdoc />
        public async Task ResumeAsync()
        {
            var resumables = new List<WeakReference<ISuspendable>>();
            var resumeTasks = new List<Task>();

            foreach (var item in this.items)
            {
                ISuspendable s;
                var targetExists = item.TryGetTarget(out s);
                if (targetExists)
                {
                    resumables.Add(item);

                    var resumeTask = s.ResumeAsync();
                    if (resumeTask == null)
                    {
                        continue;
                    }

                    if (!resumeTask.IsFaulted)
                    {
                        resumeTasks.Add(resumeTask);
                    }
                    else
                    {
                        resumables.Remove(item);
                    }
                }
                else
                {
                    resumables.Remove(item);
                }
            }

            await Task.WhenAll(resumeTasks);
        }
    }
}