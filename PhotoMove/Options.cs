using CommandLine;

namespace PhotoMove
{
    internal class Options
    {
        [Option('R', "recursive", Required = false, HelpText = "Traverse subdirectories for files.")]
        public bool Recursive { get; set; }

        [Option('r', "rename", Required = false, HelpText = "Rename files to prepend device ID.")]
        public bool Rename { get; set; }

        [Option('m', "move", Required = false, HelpText = "Actually move the files.")]
        public bool Move { get; set; }

        [Option('p', "path", Required = false, HelpText = "Path to organize.")]
        public string Path { get; set; }
    }
}
