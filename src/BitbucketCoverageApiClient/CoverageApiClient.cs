using System.Collections.Generic;
using System.Linq;
using BitbucketCoverageApiClient.Bitbucket;
using BitbucketCoverageApiClient.Parser;

namespace BitbucketCoverageApiClient
{
    /// <inheritdoc />
    public class CoverageApiClient : ICoverageApiClient
    {
        private readonly Client _client;
        private readonly ParserFactory _parserFactory = new ParserFactory();

        /// <summary>
        ///     Constructor
        /// </summary>
        public CoverageApiClient(string bitbucketBaseUrl, string bitbucketUsername, string bitbucketPassword)
        {
            _client = new Client(bitbucketBaseUrl, bitbucketUsername, bitbucketPassword);
        }

        /// <inheritdoc />
        public List<FileCoverageInfo> ReadAndUpload(string coverageFilePath, CoverageFileFormat format,
            string commitHash)
        {
            var parser = _parserFactory.CreateParser(format);
            var result = parser.Parse(coverageFilePath);
            var bitbucketModel = new Files
            {
                FilesList = result.Select(res => new FileCoverageInfo
                {
                    Path = GetPath(res),
                    Coverage = GetCoverage(res)
                }).ToList()
            };
            return _client.Upload(commitHash, bitbucketModel);
        }

        private static string GetCoverage(FileCoverageModel model)
        {
            var strings = new[]
            {
                $"C:{string.Join(',', model.CoveredLines)}",
                $"P:{string.Join(',', model.PartlyCoveredLines)}",
                $"U:{string.Join(',', model.UncoveredLines)}"
            };

            return string.Join(';', strings);
        }

        private static string GetPath(FileCoverageModel model)
        {
            return model.RelativeFilename?.Replace(@"\", "/");
        }
    }
}