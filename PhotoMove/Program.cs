// See https://aka.ms/new-console-template for more information
using CommandLine;
using PhotoMove;

Options options;

options = Parser.Default.ParseArguments<Options>(args).Value;


string path = Directory.GetCurrentDirectory();
if (options.Path != null)
{
    path = options.Path;
}

var worker = new Worker(options);
Directory.CreateDirectory(Path.Combine(path, "sorted"));
int totalCount = worker.DoWork(path, Path.Combine(path, "sorted"));

Console.WriteLine($"Moved {totalCount} files.");