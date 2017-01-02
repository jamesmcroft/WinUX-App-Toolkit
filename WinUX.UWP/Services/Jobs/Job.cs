namespace WinUX.UWP.Services.Jobs
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a base for creating jobs for the <see cref="JobManager"/>.
    /// </summary>
    public abstract class Job
    {
        private readonly WeakReference<Func<Task>> jobAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="Job"/> class.
        /// </summary>
        /// <param name="name">
        /// The name of the job.
        /// </param>
        /// <param name="action">
        /// The action to perform.
        /// </param>
        /// <param name="jobOccurence">
        /// The occurence to run the job.
        /// </param>
        protected Job(string name, Func<Task> action, TimeSpan jobOccurence)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            if (action == null) throw new ArgumentNullException(nameof(action));

            this.Name = name;
            this.jobAction = new WeakReference<Func<Task>>(action);
            this.JobOccurence = jobOccurence;

            this.LastRun = DateTime.MinValue;
        }

        /// <summary>
        /// Gets the name of the job.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the last time the job ran.
        /// </summary>
        public DateTime LastRun { get; private set; }

        /// <summary>
        /// Gets the occurence the job should run on.
        /// </summary>
        public TimeSpan JobOccurence { get; }

        /// <summary>
        /// Sets the last time the job ran.
        /// </summary>
        /// <param name="timeLastRun">
        /// The time last run.
        /// </param>
        internal void SetLastRun(DateTime timeLastRun)
        {
            this.LastRun = timeLastRun;
        }

        /// <summary>
        /// Runs the job.
        /// </summary>
        /// <returns>
        /// Returns an await-able task.
        /// </returns>
        internal async Task RunAsync()
        {
            if (this.jobAction == null)
            {
                throw new JobReferenceLostException();
            }

            Func<Task> runableAction;
            if (!this.jobAction.TryGetTarget(out runableAction))
            {
                throw new JobReferenceLostException();
            }

            await runableAction();

            this.LastRun = DateTime.Now;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// Returns the job's name.
        /// </returns>
        public override string ToString() => this.Name;
    }
}