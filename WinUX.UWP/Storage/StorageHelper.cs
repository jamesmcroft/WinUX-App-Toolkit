namespace WinUX.Storage
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Windows.ApplicationModel;
    using Windows.Storage;
    using Windows.Storage.Streams;

    /// <summary>
    /// Defines helper methods for handling storage of files.
    /// </summary>
    public class StorageHelper
    {
        private static StorageHelper current;

        /// <summary>
        /// Gets a static instance of the <see cref="StorageHelper"/>.
        /// </summary>
        public static StorageHelper Current => current ?? (current = new StorageHelper());

        /// <summary>
        /// Gets a file as a stream that is packaged as part of the application's installation folder with read access.
        /// </summary>
        /// <param name="filePath">
        /// The path to the file.
        /// </param>
        /// <returns>
        /// Returns the file stream.
        /// </returns>
        public static Task<IRandomAccessStream> GetPackagedFileAsStreamAsync(string filePath)
        {
            return GetPackagedFileAsStreamAsync(filePath, FileAccessMode.Read);
        }

        /// <summary>
        /// Gets a file as a stream that is packaged as part of the application's installation folder.
        /// </summary>
        /// <param name="filePath">
        /// The path to the file.
        /// </param>
        /// <param name="accessMode">
        /// The mode of accessing the file.
        /// </param>
        /// <returns>
        /// Returns the file stream.
        /// </returns>
        public static async Task<IRandomAccessStream> GetPackagedFileAsStreamAsync(
            string filePath,
            FileAccessMode accessMode)
        {
            var rootFolder = Package.Current.InstalledLocation;

            return await GetFileAsStreamAsync(filePath, accessMode, rootFolder);
        }

        /// <summary>
        /// Gets a file as a stream from a specified file path and root folder.
        /// </summary>
        /// <param name="filePath">
        /// The path to the file.
        /// </param>
        /// <param name="accessMode">
        /// The mode of accessing the file.
        /// </param>
        /// <param name="fileFolder">
        /// The root file folder.
        /// </param>
        /// <returns>
        /// Returns the file stream.
        /// </returns>
        public static async Task<IRandomAccessStream> GetFileAsStreamAsync(
            string filePath,
            FileAccessMode accessMode,
            StorageFolder fileFolder)
        {
            var fileName = Path.GetFileName(filePath);
            fileFolder = await GetSubFolder(filePath, fileFolder);

            var file = await fileFolder.GetFileAsync(fileName);

            return await file.OpenAsync(accessMode);
        }


        /// <summary>
        /// Saves a byte array to a <see cref="StorageFile"/> in the Temp storage folder of the application with a given file extension.
        /// </summary>
        /// <param name="bytes">
        /// The byte array to save.
        /// </param>
        /// <param name="extension">
        /// The file extension.
        /// </param>
        /// <returns>
        /// Returns the temporary <see cref="StorageFile"/> that is created.
        /// </returns>
        public async Task<StorageFile> SaveBytesToTempFolderAsync(byte[] bytes, string extension)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            var fileName = $"{Guid.NewGuid()}{extension}";
            var folder = ApplicationData.Current.TemporaryFolder;
            var tempFile = await folder.CreateFileAsync(fileName);
            await FileIO.WriteBytesAsync(tempFile, bytes);

            return tempFile;
        }

        /// <summary>
        /// Saves a string value to a <see cref="StorageFile"/> in the Temp storage folder of the application with a given file extension.
        /// </summary>
        /// <param name="content">
        /// The string value to save.
        /// </param>
        /// <param name="extension">
        /// The file extension.
        /// </param>
        /// <returns>
        /// Returns the temporary <see cref="StorageFile"/> that is created.
        /// </returns>
        public async Task<StorageFile> SaveTextToTempFolderAsync(string content, string extension)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            var fileName = $"{Guid.NewGuid()}{extension}";
            var folder = ApplicationData.Current.TemporaryFolder;
            var tempFile = await folder.CreateFileAsync(fileName);
            await FileIO.WriteTextAsync(tempFile, content);

            return tempFile;
        }

        /// <summary>
        /// Saves a byte array to a <see cref="StorageFile"/> in the given <see cref="StorageFolder"/> of the application with a given file name.
        /// </summary>
        /// <remarks>
        /// The file name provided must also contain the extension.
        /// </remarks>
        /// <param name="folder">
        /// The <see cref="StorageFolder"/> to save the file to.
        /// </param>
        /// <param name="bytes">
        /// The byte array to save.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// Returns the <see cref="StorageFile"/> that is created.
        /// </returns>
        public async Task<StorageFile> SaveBytesToFolderAsync(StorageFolder folder, byte[] bytes, string fileName)
        {
            if (folder == null)
            {
                throw new ArgumentNullException(nameof(folder));
            }

            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var file = await folder.CreateFileAsync(fileName);
            await FileIO.WriteBytesAsync(file, bytes);

            return file;
        }

        /// <summary>
        /// Saves a string value to a <see cref="StorageFile"/> in the given <see cref="StorageFolder"/> of the application with a given file name.
        /// </summary>
        /// <param name="folder">
        /// The <see cref="StorageFolder"/> to save the file to.
        /// </param>
        /// <param name="text">
        /// The string value to save.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// Returns the <see cref="StorageFile"/> that is created.
        /// </returns>
        public async Task<StorageFile> SaveTextToFolderAsync(StorageFolder folder, string text, string fileName)
        {
            if (folder == null)
            {
                throw new ArgumentNullException(nameof(folder));
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var file = await folder.CreateFileAsync(fileName);
            await FileIO.WriteTextAsync(file, text);

            return file;
        }

        /// <summary>
        /// Retrieves a <see cref="StorageFile"/> from a given <see cref="StorageFolders"/> string with a given file name.
        /// </summary>
        /// <remarks>
        /// The create if not exists boolean parameter will create the <see cref="StorageFile"/> with the given file name in the location if true.
        /// </remarks>
        /// <param name="location">
        /// The <see cref="StorageFolders"/> string.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="createIfNotExists">
        /// Whether to create the file if it doesn't exist.
        /// </param>
        /// <returns>
        /// Returns the StorageFile, if exists.
        /// </returns>
        public async Task<StorageFile> RetrieveStorageFileAsync(
            string location,
            string fileName,
            bool createIfNotExists)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var folder = await this.RetrieveStorageFolderAsync(location);

            var file = await GetFileAsync(folder, fileName);
            if (file != null || !createIfNotExists)
            {
                return file;
            }

            file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

            return file;
        }

        /// <summary>
        /// Retrieves a <see cref="StorageFolder"/> for a given <see cref="StorageFolders"/> string.
        /// </summary>
        /// <param name="location">
        /// The <see cref="StorageFolders"/> string to retrieve a <see cref="StorageFolder"/> for.
        /// </param>
        /// <returns>
        /// Returns a <see cref="StorageFolder"/>.
        /// </returns>
        public async Task<StorageFolder> RetrieveStorageFolderAsync(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                throw new ArgumentNullException(nameof(location));
            }

            var folder = ApplicationData.Current.LocalFolder;
            switch (location)
            {
                case StorageFolders.Temp:
                    folder = ApplicationData.Current.TemporaryFolder;
                    break;
                default:
                    folder = await folder.CreateFolderAsync(location, CreationCollisionOption.OpenIfExists);
                    break;
            }

            return folder;
        }

        /// <summary>
        /// Retrieves a string value from a <see cref="StorageFile"/> at the given file path.
        /// </summary>
        /// <param name="filePath">
        /// The file path of the <see cref="StorageFile"/> to retrieve data from.
        /// </param>
        /// <returns>
        /// Returns the stored string value of the <see cref="StorageFile"/>.
        /// </returns>
        public async Task<string> GetTextFromFileAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace("filePath"))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            var file = await StorageFile.GetFileFromPathAsync(filePath);
            var textContent = await FileIO.ReadTextAsync(file);

            return textContent;
        }

        /// <summary>
        /// Retrieves a byte array from a <see cref="StorageFile"/> at the given file path.
        /// </summary>
        /// <param name="filePath">
        /// The file path of the <see cref="StorageFile"/> to retrieve data from.
        /// </param>
        /// <returns>
        /// Returns the stored byte array of the <see cref="StorageFile"/>.
        /// </returns>
        public async Task<byte[]> GetByteArrayFromFileAsync(string filePath)
        {
            filePath = filePath.Replace('/', '\\');
            var file = await StorageFile.GetFileFromPathAsync(filePath);

            if (file == null)
            {
                throw new FileNotFoundException($"Unable to find file at {filePath}");
            }

            using (IRandomAccessStream stream = await file.OpenReadAsync())
            {
                using (var reader = new DataReader(stream.GetInputStreamAt(0)))
                {
                    await reader.LoadAsync((uint)stream.Size);
                    var bytes = new byte[stream.Size];
                    reader.ReadBytes(bytes);
                    return bytes;
                }
            }
        }

        private static async Task<StorageFile> GetFileAsync(StorageFolder folder, string fileName)
        {
            var files = await folder.GetFilesAsync();
            var file = files.FirstOrDefault(x => x.Name == fileName);
            return file;
        }

        private static async Task<StorageFolder> GetSubFolder(string filePath, StorageFolder fileFolder)
        {
            var folderName = Path.GetDirectoryName(filePath);

            return !string.IsNullOrEmpty(folderName) && folderName != @"\"
                       ? await fileFolder.GetFolderAsync(folderName)
                       : fileFolder;
        }
    }
}