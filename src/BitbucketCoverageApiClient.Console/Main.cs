using System;
using System.IO;
using System.Linq;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BitbucketCoverageApiClient.Console
{
    internal class Main
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<Main> _logger;

        public Main()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

            _configuration = builder.Build();
            _loggerFactory = LoggerFactory.Create(builder => builder
                .AddSimpleConsole(options => options.TimestampFormat = "[HH:mm:ss] ")
                .AddConfiguration(_configuration.GetSection("Logging")));
            _logger = _loggerFactory.CreateLogger<Main>();
        }

        public int Run(string[] args)
        {
            _logger.LogInformation("Started client");

            try
            {
                var commandlineParseResult = CommandLine.Parser.Default
                    .ParseArguments<CommandLineParameter>(args)
                    .WithParsed(Process);

                if (commandlineParseResult.Tag == ParserResultType.NotParsed)
                {
                    _logger.LogWarning("Parsing arguments failed");
                    return 2;
                };

                _logger.LogInformation("Processing was successful");
                return 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while executing Main");
                return 1;
            }
        }

        private void Process(CommandLineParameter p)
        {
            _logger.LogInformation("Processing started");

            var client = new CoverageApiClient(p.BitbucketBaseUrl, p.BitbucketUsername, p.BitbucketPassword, _loggerFactory);
            client.ReadAndUpload(p.CoverageFiles.Select(path => new CoverageFile
            {
                FileFormat = p.CoverageFileFormat,
                FilePath = path
            }), p.CommitHash);
        }
    }
}