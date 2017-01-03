namespace WinUX.UWP.Samples.Components
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using WinUX.Data.Serialization;
    using WinUX.Storage;

    public class SampleService
    {
        private readonly List<SampleCollection> sampleCollections;

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleService"/> class.
        /// </summary>
        public SampleService()
        {
            this.sampleCollections = new List<SampleCollection>();
        }

        /// <summary>
        /// Initializes the sample services.
        /// </summary>
        /// <returns>
        /// Returns a task.
        /// </returns>
        public async Task InitializeAsync()
        {
            await this.UpdateSamples();

            var tasks =
                (from collection in this.sampleCollections
                 from sample in collection.Samples
                 select sample.InitializeAsync()).ToList();

            await Task.WhenAll(tasks);
        }

        private async Task UpdateSamples()
        {
            this.sampleCollections.Clear();

            using (var stream = await StorageHelper.GetPackagedFileAsStreamAsync("Samples/samples.json"))
            {
                var json = await stream.ReadAsStringAsync();

                var samples = SerializationService.Json.Deserialize<SampleCollection[]>(json);
                this.sampleCollections.AddRange(samples);
            }
        }

        /// <summary>
        /// Gets the sample collections.
        /// </summary>
        public IReadOnlyList<SampleCollection> SampleCollections => this.sampleCollections;
    }
}