using CommandLine;

namespace BitbucketCoverageApiClient.Console
{
    public class CommandLineParameter
    {
        [Option('f', "coverageFile", Required = true, HelpText = "Path to coverage file")]
        public string CoverageFilePath { get; set; }

        [Option('t', "format", Required = true, HelpText = "The format of the coverage file to be parsed")]
        public CoverageFileFormat CoverageFileFormat { get; set; }

        [Option('s', "url", Required = true, HelpText = "Base url from bitbucket server")]
        public string BitbucketBaseUrl { get; set; }

        [Option('u', "username", Required = true, HelpText = "Bitbucket username")]
        public string BitbucketUsername { get; set; }

        [Option('p', "password", Required = true, HelpText = "Bitbucket user password")]
        public string BitbucketPassword { get; set; }

        [Option('c', "commitHash", Required = true, HelpText = "Git commit hash where coverage belongs to")]
        public string CommitHash { get; set; }
    }
}