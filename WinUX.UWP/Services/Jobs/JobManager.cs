namespace WinUX.UWP.Services.Jobs
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Windows.UI.Xaml;

    using WinUX.UWP.Diagnostics.Tracing;

    /// <summary>
    /// Defines a service for managing jobs on a timer.
    /// </summary>
    public sealed class JobManager
    {
        private static JobManager current;

        /// <summary>
        /// Gets an instance of the <see cref="JobManager"/>.
        /// </summary>
        public static JobManager Current => current ?? (current = new JobManager());

        private readonly DispatcherTimer timer;

        private readonly List<Job> jobs = new List<Job>();

        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);

        /// <summary>
        /// Initializes a new instance of the <see cref="JobManager"/> class.
        /// </summary>
        public JobManager()
        {
            this.timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 5) };
            this.timer.Tick += this.Timer_OnTick;
            this.timer.Start();
        }

        /// <summary>
        /// Adds a job to the manager.
        /// </summary>
        /// <param name="job">
        /// The job to add.
        /// </param>
        /// <returns>
        /// Returns an await-able task.
        /// </returns>
        public async Task AddJobAsync(Job job)
        {
            await this.semaphore.WaitAsync();

            try
            {
                if (!this.jobs.Contains(job))
                {
                    this.jobs.Add(job);
                }
            }
            finally
            {
                this.semaphore.Release();
            }
        }

        /// <summary>
        /// Removes a job from the manager.
        /// </summary>
        /// <param name="job">
        /// The job to remove.
        /// </param>
        /// <returns>
        /// Returns an await-able task.s
        /// </returns>
        public async Task RemoveJobAsync(Job job)
        {
            await this.semaphore.WaitAsync();

            try
            {
                if (this.jobs.Contains(job))
                {
                    this.jobs.Remove(job);
                }
            }
            finally
            {
                this.semaphore.Release();
            }
        }

        private void Timer_OnTick(object sender, object o)
        {
            this.RunJobs();
        }

        private async void RunJobs()
        {
            await this.semaphore.WaitAsync();

            try
            {
                var redundantJobs = new List<Job>();

                foreach (var job in this.jobs)
                {
                    if (DateTime.Now.Subtract(job.JobOccurence) > job.LastRun)
                    {
                        try
                        {
                            await job.RunAsync();
                        }
                        catch (JobReferenceLostException)
                        {
                            redundantJobs.Add(job);
                        }
                        catch (Exception ex)
                        {
                            EventLogger.Current.WriteError(ex.Message);
                        }
                    }
                }

                foreach (var redundantJob in this.jobs)
                {
                    this.jobs.Remove(redundantJob);
                }
            }
            finally
            {
                this.semaphore.Release();
            }
        }
    }
}