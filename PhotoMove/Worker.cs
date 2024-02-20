namespace PhotoMove
{
    internal class Worker
    {
        public Options Options { get; private set; }
        public int TotalFileCount { get; private set; }

        public Worker(Options options)
        {
            Options = options;
        }

        public int DoWork(string path, string dest)
        {
            int count = 0;

            var imageProcessor = new ImageProcessor(Options);
            var quicktimeProcessor = new QuicktimeProcessor(Options);
            var directories = Directory.GetDirectories(path);

            foreach (var filePath in Directory.GetFiles(path))
            {
                var file = new FileInfo(filePath);
                IReadOnlyList<MetadataExtractor.Directory> dirs;
                string dateTimeText = "";

                if (file.Extension.Equals(".yml", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                if (file.Extension.Equals(".db", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                else if (imageProcessor.TryProcess(dest, filePath))
                {
                    count++;
                }
                else if (quicktimeProcessor.TryProcess(dest, filePath))
                {
                    count++;
                }
                TotalFileCount++;
            }

            if (Options.Recursive)
            {
                foreach (var directory in directories)
                {
                    if (directory != dest)
                    {
                        count += DoWork(directory, dest);
                    }
                }
            }

            return count;
        }
    }
}
