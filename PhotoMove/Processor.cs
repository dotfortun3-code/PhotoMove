using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.QuickTime;

namespace PhotoMove
{

    internal abstract class Processor
    {
        private static int[] DATE_TAG_IDS = new int[] {
            QuickTimeMovieHeaderDirectory.TagCreated, ExifDirectoryBase.TagDateTimeOriginal, ExifDirectoryBase.TagDateTime,
            QuickTimeMetadataHeaderDirectory.TagCreationDate
        };

        private static int[] DEVICE_TAG_IDS = new int[]
        {
            ExifDirectoryBase.TagCameraOwnerName, ExifDirectoryBase.TagMake, ExifDirectoryBase.TagModel,
            QuickTimeMetadataHeaderDirectory.TagArtist, QuickTimeMetadataHeaderDirectory.TagMake,
            QuickTimeMetadataHeaderDirectory.TagModel,

        };

        public Options Options { get; set; }

        public Processor(Options options)
        {
            Options = options;
        }

        public bool Move(DateTime date, string rootPath, string filePath, string name)
        {

            string year = date.Year.ToString();
            string month = date.Month.ToString("00");
            string day = date.Day.ToString("00");

            string newPath = Path.Combine(rootPath, year);
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            newPath = Path.Combine(rootPath, year, month);
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            newPath = Path.Combine(rootPath, year, month, day);
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            newPath = Path.Combine(rootPath, year, month, day, name);

            try
            {
                File.Move(filePath, newPath, false);
                Console.WriteLine($"File moved from {filePath} to {newPath}.");
                return true;
            }
            catch (IOException iox)
            {
                return false;
            }
        }

        public static string TryGetDeviceInfo(IReadOnlyList<MetadataExtractor.Directory> directories)
        {
            string? make = null;
            string? model = null;

            foreach (var d in directories)
            {
                try
                {
                    make = make ?? d.GetDescription(ExifDirectoryBase.TagMake);
                    model = model ?? d.GetDescription(ExifDirectoryBase.TagModel);

                    make = make ?? d.GetDescription(QuickTimeMetadataHeaderDirectory.TagMake);
                    model = model ?? d.GetDescription(QuickTimeMetadataHeaderDirectory.TagModel);

                    if (make != null && model != null)
                    {
                        break;
                    }
                }
                catch (Exception ex) { }

            }


            make = make ?? "unknown";
            model = model ?? "unknown";

            string deviceDetails = $"{make}-{model}";
            deviceDetails = deviceDetails.Replace(" ", ".").ToLower();

            return deviceDetails;
        }

        public static DateTime TryGetDateTime(IReadOnlyList<MetadataExtractor.Directory> directories)
        {
            foreach (var d in directories)
            {
                foreach (var t in DATE_TAG_IDS)
                {
                    try
                    {
                        if (MetadataExtractor.DirectoryExtensions.TryGetDateTime(d, t, out DateTime date))
                        {
                            return date;
                        }
                    }
                    catch (Exception ex) { }
                }
            }

            return DateTime.MinValue;
        }

    }
}
