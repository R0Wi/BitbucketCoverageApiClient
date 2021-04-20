using System;
using CommandLine;

namespace BitbucketCoverageApiClient.Console
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var commandlineParseResult = CommandLine.Parser.Default
                    .ParseArguments<CommandLineParameter>(args)
                    .WithParsed(Process);

                return commandlineParseResult.Tag == ParserResultType.NotParsed ? 2 : 0;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                return 1;
            }
        }

        private static void Process(CommandLineParameter p)
        {
            var client = new CoverageApiClient(p.BitbucketBaseUrl, p.BitbucketUsername, p.BitbucketPassword);
            client.ReadAndUpload(p.CoverageFilePath, p.CoverageFileFormat, p.CommitHash);
        }
    }
}