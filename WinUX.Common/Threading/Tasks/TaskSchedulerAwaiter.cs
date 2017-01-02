namespace WinUX.Threading.Tasks
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a task scheduler awaiter that can be used for Xamarin development.
    /// </summary>
    public struct TaskSchedulerAwaiter : INotifyCompletion
    {
        private readonly TaskScheduler taskScheduler;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskSchedulerAwaiter"/> struct.
        /// </summary>
        /// <param name="taskScheduler">
        /// The task scheduler.
        /// </param>
        internal TaskSchedulerAwaiter(TaskScheduler taskScheduler)
        {
            this.taskScheduler = taskScheduler;
        }

        /// <summary>
        /// Gets the <see cref="TaskSchedulerAwaiter"/>.
        /// </summary>
        /// <returns>
        /// Returns this instance.
        /// </returns>
        public TaskSchedulerAwaiter GetAwaiter()
        {
            return this;
        }

        /// <summary>
        /// Gets a value indicating whether the awaiter is completed.
        /// </summary>
        public bool IsCompleted => this.taskScheduler == null;

        /// <summary>
        /// Gets a new instance of a <see cref="TaskSchedulerAwaiter"/>.
        /// </summary>
        /// <returns>
        /// Returns an instance of <see cref="TaskSchedulerAwaiter"/>.
        /// </returns>
        public static TaskSchedulerAwaiter NewTaskSchedulerAwaiter()
        {
            return new TaskSchedulerAwaiter(SynchronizationContext.Current != null ? TaskScheduler.Default : null);
        }

        /// <summary>
        /// Schedules the continuation action that's invoked when the instance completes.
        /// </summary>
        /// <param name="completedAction">
        /// The action to invoke when the operation completes.
        /// </param>
        public void OnCompleted(Action completedAction)
        {
            if (this.taskScheduler == null)
            {
                throw new InvalidOperationException("The task scheduler has been disposed.");
            }

            Task.Factory.StartNew(completedAction, CancellationToken.None, TaskCreationOptions.None, this.taskScheduler);
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        public void GetResult()
        {
        }
    }
}