using MetadataExtractor.Formats.QuickTime;

namespace PhotoMove
{
    internal class QuicktimeProcessor : Processor
    {
        public QuicktimeProcessor(Options options) : base(options) { }
        public bool TryProcess(string rootPath, string filePath)
        {
            var file = new FileInfo(filePath);
            var date = DateTime.MinValue;
            string detailName = "";

            using (var fs = File.OpenRead(filePath))
            {
                var dirs = MetadataExtractor.Formats.QuickTime.QuickTimeMetadataReader.ReadMetadata(fs);

                var subDir = dirs.OfType<QuickTimeMetadataHeaderDirectory>().FirstOrDefault();
                date = TryGetDateTime(dirs);

                if (Options.Rename)
                {
                    detailName = TryGetDeviceInfo(dirs);
                }
            }
            string name = file.Name;
            if (Options.Rename)
            {
                name = $"{detailName}.{file.Name}";
            }

            if (Options.Move)
            {
                return Move(date, rootPath, filePath, name);
            }
            else
            {
                string newPathName = Path.Combine(rootPath, name);

                file.MoveTo(newPathName);

                Console.WriteLine($"File renamed from {file.Name} to {name}.");
            }

            return true;
        }
    }
}

