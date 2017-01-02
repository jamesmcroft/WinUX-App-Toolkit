namespace WinUX.UWP.Storage.Streams
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Windows.ApplicationModel;
    using Windows.Storage;
    using Windows.Storage.Streams;

    /// <summary>
    /// Defines a collection of helper methods for handling storage file streams.
    /// </summary>
    public static class StorageStreamHelper
    {
        /// <summary>
        /// Return a stream to a specified file from the application's installation folder using the Read access mode.
        /// </summary>
        /// <param name="filePath">
        /// The path of the file to retrieve.
        /// </param>
        /// <returns>
        /// Returns the file stream.
        /// </returns>
        public static Task<IRandomAccessStream> GetPackagedFileStreamAsync(string filePath)
        {
            return GetPackagedFileStreamAsync(filePath, FileAccessMode.Read);
        }

        /// <summary>
        /// Return a stream to a specified file from the application's installation folder.
        /// </summary>
        /// <param name="filePath">
        /// The path of the file to retrieve.
        /// </param>
        /// <param name="accessMode">
        /// The file access mode.
        /// </param>
        /// <returns>
        /// Returns the file stream.
        /// </returns>
        public static async Task<IRandomAccessStream> GetPackagedFileStreamAsync(
            string filePath,
            FileAccessMode accessMode)
        {
            var rootFolder = Package.Current.InstalledLocation;

            return await GetFileStreamAsync(filePath, accessMode, rootFolder);
        }

        /// <summary>
        /// Return a stream to a specified file.
        /// </summary>
        /// <param name="filePath">
        /// The path of the file to retrieve.
        /// </param>
        /// <param name="accessMode">
        /// The file access mode.
        /// </param>
        /// <param name="fileFolder">
        /// The file folder.
        /// </param>
        /// <returns>
        /// Returns the file stream.
        /// </returns>
        public static async Task<IRandomAccessStream> GetFileStreamAsync(
            string filePath,
            FileAccessMode accessMode,
            StorageFolder fileFolder)
        {
            var fileName = Path.GetFileName(filePath);
            fileFolder = await ExtractSubFolder(filePath, fileFolder);

            var file = await fileFolder.GetFileAsync(fileName);

            return await file.OpenAsync(accessMode);
        }

        private static async Task<StorageFolder> ExtractSubFolder(string filePath, StorageFolder fileFolder)
        {
            var folderName = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(folderName) && folderName != @"\")
            {
                return await fileFolder.GetFolderAsync(folderName);
            }

            return fileFolder;
        }
    }
}