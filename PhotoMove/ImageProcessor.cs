using System.Diagnostics;

namespace PhotoMove
{
    internal class ImageProcessor : Processor
    {
        public ImageProcessor(Options options) : base(options) { }

        public bool TryProcess(string rootPath, string filePath)
        {
            try
            {
                var file = new FileInfo(filePath);
                var dirs = MetadataExtractor.ImageMetadataReader.ReadMetadata(filePath);
                string detailName = "";

                var date = TryGetDateTime(dirs);
                if (date == DateTime.MinValue)
                {
                    if (file.Extension.Equals("mov", StringComparison.OrdinalIgnoreCase))
                    {
                        Debugger.Break();
                    }
                    date = file.CreationTime;
                }

                if (Options.Rename)
                {
                    detailName = TryGetDeviceInfo(dirs);
                }

                string name = file.Name;
                if (Options.Rename && detailName != "unknown-unknown")
                {
                    name = $"{detailName}.{file.Name}";
                }
                string newPathName = Path.Combine(rootPath, name);
                if (Options.Move)
                {

                    return Move(date, rootPath, filePath, name);
                }
                else
                {
                    file.MoveTo(newPathName);

                    Console.WriteLine($"File renamed from {file.Name} to {name}.");
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
