namespace WinUX.UWP.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Windows.Storage;
    using Windows.Storage.FileProperties;
    using Windows.Storage.Streams;

    using WinUX.Common;
    using WinUX.Storage;
    using WinUX.UWP.Storage;

    /// <summary>
    /// Defines a collection of extensions for <see cref="StorageFile"/>.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Gets the byte data from a <see cref="StorageFile"/>.
        /// </summary>
        /// <param name="storageFile">
        /// The <see cref="StorageFile"/>.
        /// </param>
        /// <returns>
        /// Returns the bytes representing the <see cref="StorageFile"/>.
        /// </returns>
        public static async Task<byte[]> ToBytesAsync(this StorageFile storageFile)
        {
            byte[] bytes;
            using (var stream = await storageFile.OpenReadAsync())
            {
                bytes = new byte[stream.Size];
                using (var reader = new DataReader(stream))
                {
                    await reader.LoadAsync((uint)stream.Size);
                    reader.ReadBytes(bytes);
                }
            }

            return bytes;
        }

        /// <summary>
        /// Gets a collection of basic properties from a StorageFile.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        /// <returns>
        /// Returns a collection of StorageFileProperty.
        /// </returns>
        public static async Task<IEnumerable<StorageFileProperty>> GetPropertiesAsync(this StorageFile file)
        {
            var properties = new List<StorageFileProperty>();

            GetDocumentProperties(properties, await file.Properties.GetDocumentPropertiesAsync());
            GetImageProperties(properties, await file.Properties.GetImagePropertiesAsync());
            GetMusicProperties(properties, await file.Properties.GetMusicPropertiesAsync());
            GetVideoProperties(properties, await file.Properties.GetVideoPropertiesAsync());

            return properties;
        }

        /// <summary>
        /// Gets extended properties for a given <see cref="StorageFile"/>.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<IEnumerable<StorageFileProperty>> GetExtendedPropertiesAsync(this StorageFile file)
        {
            var properties = new List<StorageFileProperty>();

            var fileProps = await file.Properties.RetrievePropertiesAsync(null);

            ProcessExtendedProperties(properties, fileProps);

            return properties;
        }

        private static void ProcessExtendedProperties(
            ICollection<StorageFileProperty> results,
            IDictionary<string, object> props)
        {
            if (props.ContainsKey("System.ItemName"))
            {
                var fileNameObj = props["System.ItemName"];
                var fileName = fileNameObj?.ToString();
                if (!string.IsNullOrWhiteSpace(fileName) && results.FirstOrDefault(x => x.Name == "File name") == null) results.Add(new StorageFileProperty("File name", fileName));
            }

            if (props.ContainsKey("System.FileOwner"))
            {
                var fileOwnerObj = props["System.FileOwner"];
                var fileOwner = fileOwnerObj?.ToString();
                if (!string.IsNullOrWhiteSpace(fileOwner) && results.FirstOrDefault(x => x.Name == "File owner") == null) results.Add(new StorageFileProperty("File owner", fileOwner));
            }

            if (props.ContainsKey("System.Size"))
            {
                var fileSizeObj = props["System.Size"];
                var fileSize = fileSizeObj?.ToString();
                if (!string.IsNullOrWhiteSpace(fileSize) && results.FirstOrDefault(x => x.Name == "File size") == null)
                    results.Add(
                        new StorageFileProperty(
                            "File size",
                            ParseHelper.SafeParseDouble(fileSize).ToFileSize(FileSizeFormat.Gigabyte)));
            }

            if (props.ContainsKey("System.DateCreated"))
            {
                var dateCreatedObj = props["System.DateCreated"];
                var dateCreated = dateCreatedObj?.ToString();
                if (!string.IsNullOrWhiteSpace(dateCreated)
                    && results.FirstOrDefault(x => x.Name == "Date created") == null) results.Add(new StorageFileProperty("Date created", dateCreated));
            }

            if (props.ContainsKey("System.Photo.DateTaken"))
            {
                var dateTakenObj = props["System.Photo.DateTaken"];
                var dateTaken = dateTakenObj?.ToString();
                if (!string.IsNullOrWhiteSpace(dateTaken) && results.FirstOrDefault(x => x.Name == "Date taken") == null) results.Add(new StorageFileProperty("Date taken", dateTaken));
            }

            if (props.ContainsKey("System.ItemTypeText"))
            {
                var itemTypeObj = props["System.ItemTypeText"];
                var itemType = itemTypeObj?.ToString();
                if (!string.IsNullOrWhiteSpace(itemType) && results.FirstOrDefault(x => x.Name == "Item type") == null) results.Add(new StorageFileProperty("Item type", itemType));
            }

            if (props.ContainsKey("System.MIMEType"))
            {
                var mimeTypeObj = props["System.MIMEType"];
                var mimeType = mimeTypeObj?.ToString();
                if (!string.IsNullOrWhiteSpace(mimeType) && results.FirstOrDefault(x => x.Name == "MIME type") == null) results.Add(new StorageFileProperty("MIME type", mimeType));
            }

            ProcessPhotoProperties(results, props);
            ProcessVideoProperties(results, props);
            ProcessAudioProperties(results, props);

            if (props.ContainsKey("System.ApplicationName"))
            {
                var applicationNameObj = props["System.ApplicationName"];
                var applicationName = applicationNameObj?.ToString();
                if (!string.IsNullOrWhiteSpace(applicationName)
                    && results.FirstOrDefault(x => x.Name == "Application name") == null) results.Add(new StorageFileProperty("Application name", applicationName));
            }

            if (props.ContainsKey("System.DRM.IsProtected"))
            {
                var drmProtectedObj = props["System.DRM.IsProtected"];
                var drmProtected = drmProtectedObj?.ToString();
                if (!string.IsNullOrWhiteSpace(drmProtected)
                    && results.FirstOrDefault(x => x.Name == "DRM protected") == null) results.Add(new StorageFileProperty("DRM protected", drmProtected));
            }

            if (props.ContainsKey("System.ComputerName"))
            {
                var computerNameObj = props["System.ComputerName"];
                var computerName = computerNameObj?.ToString();
                if (!string.IsNullOrWhiteSpace(computerName)
                    && results.FirstOrDefault(x => x.Name == "Originating PC") == null) results.Add(new StorageFileProperty("Originating PC", computerName));
            }

            ProcessGpsProperties(results, props);
        }

        private static void ProcessPhotoProperties(
            ICollection<StorageFileProperty> results,
            IDictionary<string, object> props)
        {
            if (props.ContainsKey("System.Image.Dimensions"))
            {
                var imageDimensionsObj = props["System.Image.Dimensions"];
                var imageDimensions = imageDimensionsObj?.ToString();
                if (!string.IsNullOrWhiteSpace(imageDimensions)
                    && results.FirstOrDefault(x => x.Name == "Image dimensions") == null) results.Add(new StorageFileProperty("Image dimensions", imageDimensions));
            }

            if (props.ContainsKey("System.Photo.CameraManufacturer"))
            {
                var cameraManufacturerObj = props["System.Photo.CameraManufacturer"];
                var cameraManufacturer = cameraManufacturerObj?.ToString();
                if (!string.IsNullOrWhiteSpace(cameraManufacturer)
                    && results.FirstOrDefault(x => x.Name == "Camera manufacturer") == null) results.Add(new StorageFileProperty("Camera manufacturer", cameraManufacturer));
            }

            if (props.ContainsKey("System.Photo.CameraModel"))
            {
                var cameraModelObj = props["System.Photo.CameraModel"];
                var cameraModel = cameraModelObj?.ToString();
                if (!string.IsNullOrWhiteSpace(cameraModel)
                    && results.FirstOrDefault(x => x.Name == "Camera model") == null) results.Add(new StorageFileProperty("Camera model", cameraModel));
            }

            if (props.ContainsKey("System.Photo.FlashText"))
            {
                var cameraFlashObj = props["System.Photo.FlashText"];
                var cameraFlash = cameraFlashObj?.ToString();
                if (!string.IsNullOrWhiteSpace(cameraFlash)
                    && results.FirstOrDefault(x => x.Name == "Camera flash") == null) results.Add(new StorageFileProperty("Camera flash", cameraFlash));
            }

            if (props.ContainsKey("System.Photo.ISOSpeed"))
            {
                var cameraIsoObj = props["System.Photo.ISOSpeed"];
                var cameraIso = cameraIsoObj?.ToString();
                if (!string.IsNullOrWhiteSpace(cameraIso) && results.FirstOrDefault(x => x.Name == "Camera ISO") == null) results.Add(new StorageFileProperty("Camera ISO", cameraIso));
            }

            if (props.ContainsKey("System.Photo.ShutterSpeed"))
            {
                var cameraShutterSpeedObj = props["System.Photo.ShutterSpeed"];
                var cameraShutterSpeed = cameraShutterSpeedObj?.ToString();
                if (!string.IsNullOrWhiteSpace(cameraShutterSpeed)
                    && results.FirstOrDefault(x => x.Name == "Camera shutter speed") == null) results.Add(new StorageFileProperty("Camera shutter speed", cameraShutterSpeed));
            }

            if (props.ContainsKey("System.Photo.Aperture"))
            {
                var cameraApertureObj = props["System.Photo.Aperture"];
                var cameraAperture = cameraApertureObj?.ToString();
                if (!string.IsNullOrWhiteSpace(cameraAperture)
                    && results.FirstOrDefault(x => x.Name == "Camera aperture") == null) results.Add(new StorageFileProperty("Camera aperture", cameraAperture));
            }

            if (props.ContainsKey("System.Photo.ExposureTime"))
            {
                var cameraExposureObj = props["System.Photo.ExposureTime"];
                var cameraExposure = cameraExposureObj?.ToString();
                if (!string.IsNullOrWhiteSpace(cameraExposure)
                    && results.FirstOrDefault(x => x.Name == "Camera exposure time") == null) results.Add(new StorageFileProperty("Camera exposure time", cameraExposure));
            }

            if (props.ContainsKey("System.Photo.WhiteBalanceText"))
            {
                var cameraWhiteBalanceObj = props["System.Photo.WhiteBalanceText"];
                var cameraWhiteBalance = cameraWhiteBalanceObj?.ToString();
                if (!string.IsNullOrWhiteSpace(cameraWhiteBalance)
                    && results.FirstOrDefault(x => x.Name == "Camera white balance") == null) results.Add(new StorageFileProperty("Camera white balance", cameraWhiteBalance));
            }

            if (props.ContainsKey("System.Photo.DigitalZoom"))
            {
                var cameraDigitalZoomObj = props["System.Photo.DigitalZoom"];
                var cameraDigitalZoom = cameraDigitalZoomObj?.ToString();
                if (!string.IsNullOrWhiteSpace(cameraDigitalZoom)
                    && results.FirstOrDefault(x => x.Name == "Camera digital zoom level") == null) results.Add(new StorageFileProperty("Camera digital zoom level", cameraDigitalZoom));
            }
        }

        private static void ProcessVideoProperties(
            ICollection<StorageFileProperty> results,
            IDictionary<string, object> props)
        {
            if (props.ContainsKey("System.Video.FrameWidth") && props.ContainsKey("System.Video.FrameHeight"))
            {
                var frameWidthObj = props["System.Video.FrameWidth"];
                var frameHeightObj = props["System.Video.FrameHeight"];

                if (frameWidthObj != null && frameHeightObj != null)
                {
                    var frameWidth = frameWidthObj.ToString();
                    var frameHeight = frameHeightObj.ToString();

                    if ((!string.IsNullOrWhiteSpace(frameWidth) || !string.IsNullOrWhiteSpace(frameHeight))
                        && results.FirstOrDefault(x => x.Name == "Video dimensions") == null)
                        results.Add(
                            new StorageFileProperty(
                                "Video dimensions",
                                string.Format("{0} x {1}", frameWidth, frameHeight)));
                }
            }

            if (props.ContainsKey("System.Media.Duration"))
            {
                var mediaDurationObj = props["System.Media.Duration"];
                var mediaDuration = mediaDurationObj?.ToString();
                if (!string.IsNullOrWhiteSpace(mediaDuration))
                {
                    var duration = TimeSpan.FromSeconds(ParseHelper.SafeParseDouble(mediaDuration));

                    if (results.FirstOrDefault(x => x.Name == "Media duration") == null)
                    {
                        results.Add(new StorageFileProperty("Media duration", duration.ToString("g")));
                    }
                }
            }
        }

        private static void ProcessAudioProperties(
            ICollection<StorageFileProperty> results,
            IDictionary<string, object> props)
        {
            if (props.ContainsKey("System.Music.DisplayArtist"))
            {
                var audioArtistObj = props["System.Music.DisplayArtist"];
                var audioArtist = audioArtistObj?.ToString();
                if (!string.IsNullOrWhiteSpace(audioArtist)
                    && results.FirstOrDefault(x => x.Name == "Audio artist") == null) results.Add(new StorageFileProperty("Audio artist", audioArtist));
            }

            if (props.ContainsKey("System.Music.BeatsPerMinute"))
            {
                var bpmObj = props["System.Music.BeatsPerMinute"];
                var bpm = bpmObj?.ToString();
                if (!string.IsNullOrWhiteSpace(bpm) && results.FirstOrDefault(x => x.Name == "Beats-per-minute") == null) results.Add(new StorageFileProperty("Beats-per-minute", bpm));
            }

            if (props.ContainsKey("System.Media.Year"))
            {
                var mediaYearObj = props["System.Media.Year"];
                var mediaYear = mediaYearObj?.ToString();
                if (!string.IsNullOrWhiteSpace(mediaYear) && results.FirstOrDefault(x => x.Name == "Media year") == null) results.Add(new StorageFileProperty("Media year", mediaYear));
            }
        }

        private static void ProcessGpsProperties(
            ICollection<StorageFileProperty> results,
            IDictionary<string, object> props)
        {
            if (props.ContainsKey("System.GPS.LatitudeDecimal") && props.ContainsKey("System.GPS.LongitudeDecimal"))
            {
                var latitudeObj = props["System.GPS.LatitudeDecimal"];
                var longitudeObj = props["System.GPS.LongitudeDecimal"];

                if (latitudeObj != null && longitudeObj != null)
                {
                    var latitude = latitudeObj.ToString();
                    var longitude = longitudeObj.ToString();

                    if (!string.IsNullOrWhiteSpace(latitude) || !string.IsNullOrWhiteSpace(longitude))
                    {
                        if (results.FirstOrDefault(x => x.Name == "GPS latitude") == null)
                        {
                            results.Add(new StorageFileProperty("GPS latitude", latitude));
                        }

                        if (results.FirstOrDefault(x => x.Name == "GPS longitude") == null)
                        {
                            results.Add(new StorageFileProperty("GPS longitude", longitude));
                        }
                    }
                }
            }

            if (props.ContainsKey("System.GPS.Altitude"))
            {
                var gpsAltitudeObj = props["System.GPS.Altitude"];
                var gpsAltitude = gpsAltitudeObj?.ToString();
                if (!string.IsNullOrWhiteSpace(gpsAltitude)
                    && results.FirstOrDefault(x => x.Name == "GPS altitude") == null) results.Add(new StorageFileProperty("GPS altitude", gpsAltitude));
            }

            if (props.ContainsKey("System.GPS.DOP"))
            {
                var gpsDopObj = props["System.GPS.DOP"];
                var gpsDop = gpsDopObj?.ToString();
                if (!string.IsNullOrWhiteSpace(gpsDop)
                    && results.FirstOrDefault(x => x.Name == "GPS dilution of precision") == null) results.Add(new StorageFileProperty("GPS dilution of precision", gpsDop));
            }
        }

        private static void GetDocumentProperties(ICollection<StorageFileProperty> results, DocumentProperties props)
        {
            var title = props.Title;
            var authors = props.Author;
            var comment = props.Comment;

            if (!string.IsNullOrWhiteSpace(title) && results.FirstOrDefault(x => x.Name == "Title") == null) results.Add(new StorageFileProperty("Title", title));

            if (authors != null && results.FirstOrDefault(x => x.Name == "Author") == null)
            {
                foreach (var author in authors.Where(author => !string.IsNullOrWhiteSpace(author)))
                {
                    results.Add(new StorageFileProperty("Author", author));
                }
            }

            if (!string.IsNullOrWhiteSpace(comment) && results.FirstOrDefault(x => x.Name == "Comment") == null) results.Add(new StorageFileProperty("Comment", comment));
        }

        private static void GetImageProperties(ICollection<StorageFileProperty> results, ImageProperties props)
        {
            var title = props.Title;
            var dateTaken = props.DateTaken.ToString("G");

            var manufacturer = props.CameraManufacturer;
            var model = props.CameraModel;

            var height = props.Height;
            var width = props.Width;

            var latitude = props.Latitude.ToString();
            var longitude = props.Longitude.ToString();

            if (!string.IsNullOrWhiteSpace(title) && results.FirstOrDefault(x => x.Name == "Title") == null) results.Add(new StorageFileProperty("Title", title));

            if (!string.IsNullOrWhiteSpace(dateTaken) && results.FirstOrDefault(x => x.Name == "Date Taken") == null) results.Add(new StorageFileProperty("Date Taken", dateTaken));

            if (!string.IsNullOrWhiteSpace(manufacturer)
                && results.FirstOrDefault(x => x.Name == "Manufacturer") == null) results.Add(new StorageFileProperty("Manufacturer", manufacturer));

            if (!string.IsNullOrWhiteSpace(model) && results.FirstOrDefault(x => x.Name == "Model") == null) results.Add(new StorageFileProperty("Model", model));

            if (height != 0 || width != 0)
            {
                if (results.FirstOrDefault(x => x.Name == "Height") == null)
                {
                    results.Add(new StorageFileProperty("Height", height));
                }

                if (results.FirstOrDefault(x => x.Name == "Width") == null)
                {
                    results.Add(new StorageFileProperty("Width", width));
                }
            }

            if (!string.IsNullOrWhiteSpace(latitude) && results.FirstOrDefault(x => x.Name == "GPS latitude") == null) results.Add(new StorageFileProperty("GPS latitude", latitude));

            if (!string.IsNullOrWhiteSpace(longitude) && results.FirstOrDefault(x => x.Name == "GPS longitude") == null) results.Add(new StorageFileProperty("GPS longitude", longitude));
        }

        private static void GetMusicProperties(ICollection<StorageFileProperty> results, MusicProperties props)
        {
            var title = props.Title;
            var artist = props.Artist;
            var duration = props.Duration;
            var bitRate = props.Bitrate;

            if (!string.IsNullOrWhiteSpace(title) && results.FirstOrDefault(x => x.Name == "Title") == null) results.Add(new StorageFileProperty("Title", title));

            if (!string.IsNullOrWhiteSpace(artist) && results.FirstOrDefault(x => x.Name == "Artist") == null) results.Add(new StorageFileProperty("Artist", artist));

            if (duration != TimeSpan.FromSeconds(0))
            {
                var val = duration.ToString("G");
                if (!string.IsNullOrWhiteSpace(val) && results.FirstOrDefault(x => x.Name == "Duration") == null) results.Add(new StorageFileProperty("Duration", val));
            }

            if (bitRate != 0 && results.FirstOrDefault(x => x.Name == "Bitrate") == null) results.Add(new StorageFileProperty("Bitrate", bitRate));
        }

        private static void GetVideoProperties(ICollection<StorageFileProperty> results, VideoProperties props)
        {
            var title = props.Title;
            var subTitle = props.Subtitle;
            var duration = props.Duration;
            var bitRate = props.Bitrate;
            var height = props.Height;
            var width = props.Width;

            var latitude = props.Latitude.ToString();
            var longitude = props.Longitude.ToString();

            if (!string.IsNullOrWhiteSpace(title) && results.FirstOrDefault(x => x.Name == "Title") == null) results.Add(new StorageFileProperty("Title", title));

            if (!string.IsNullOrWhiteSpace(subTitle) && results.FirstOrDefault(x => x.Name == "Subtitle") == null) results.Add(new StorageFileProperty("Subtitle", subTitle));

            if (duration != TimeSpan.FromSeconds(0))
            {
                var val = duration.ToString("G");
                if (!string.IsNullOrWhiteSpace(val) && results.FirstOrDefault(x => x.Name == "Duration") == null) results.Add(new StorageFileProperty("Duration", val));
            }

            if (bitRate != 0 && results.FirstOrDefault(x => x.Name == "Bitrate") == null) results.Add(new StorageFileProperty("Bitrate", bitRate));

            if (height != 0 || width != 0)
            {
                if (results.FirstOrDefault(x => x.Name == "Height") == null)
                {
                    results.Add(new StorageFileProperty("Height", height));
                }

                if (results.FirstOrDefault(x => x.Name == "Width") == null)
                {
                    results.Add(new StorageFileProperty("Width", width));
                }
            }

            if (!string.IsNullOrWhiteSpace(latitude) && results.FirstOrDefault(x => x.Name == "GPS latitude") == null) results.Add(new StorageFileProperty("GPS latitude", latitude));

            if (!string.IsNullOrWhiteSpace(longitude) && results.FirstOrDefault(x => x.Name == "GPS longitude") == null) results.Add(new StorageFileProperty("GPS longitude", longitude));
        }
    }
}