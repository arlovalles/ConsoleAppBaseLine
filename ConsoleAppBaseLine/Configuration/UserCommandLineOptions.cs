using CommandLine;

namespace ConsoleAppBaseLine.Configuration
{
    public class UserCommandLineOptions
    {
        [Option('m', "mode", Required = false, HelpText = "Run Mode. ")]
        public RunMode RunMode { get; set; }

        [Option('i', "inputFiles", Required = false, HelpText = "Input files to be processed.")]
        public IEnumerable<string> InputFiles { get; set; }

        [Option('o', "outputFile", Required = false, HelpText = "Output file to be created.")]
        public string OutputFile { get; set; }

    }
}
