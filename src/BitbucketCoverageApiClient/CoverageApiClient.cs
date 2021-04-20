using System.Collections.Generic;
using System.Linq;
using BitbucketCoverageApiClient.Bitbucket;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace BitbucketCoverageApiClient
{
    /// <inheritdoc />
    public class CoverageApiClient : ICoverageApiClient
    {
        private readonly IClient _client;
        private readonly ICoverageFileReader _coverageFileReader;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<CoverageApiClient> _logger;

        /// <summary>
        ///     Constructor
        /// </summary>
        public CoverageApiClient(string bitbucketBaseUrl, string bitbucketUsername, string bitbucketPassword, ILoggerFactory loggerFactory = null) : this (new Client(bitbucketBaseUrl, bitbucketUsername, bitbucketPassword), new CoverageFileReader(), loggerFactory)
        {
            
        }

        /// <summary>
        ///     Constructor for testing only
        /// </summary>
        internal CoverageApiClient(IClient client, ICoverageFileReader coverageFileReader, ILoggerFactory loggerFactory = null)
        {
            _client = client;
            _coverageFileReader = coverageFileReader;
            _loggerFactory = loggerFactory ?? NullLoggerFactory.Instance;
            _logger = _loggerFactory.CreateLogger<CoverageApiClient>();
        }

        /// <inheritdoc />
        public List<FileCoverageInfo> ReadAndUpload(string coverageFilePath, CoverageFileFormat format, string commitHash) => ReadAndUploadSingleFile(new CoverageFile 
        {
            FilePath = coverageFilePath,
            FileFormat = format
        }, commitHash);

        /// <inheritdoc />
        public List<List<FileCoverageInfo>> ReadAndUpload(IEnumerable<CoverageFile> files, string commitHash) => _coverageFileReader
            .ReadFiles(files)
            .ToList() // Read and validate all files before upload
            .Select(f => _client.Upload(commitHash, f))
            .ToList();

        private List<FileCoverageInfo> ReadAndUploadSingleFile(CoverageFile file, string commitHash) => _client.Upload(commitHash, _coverageFileReader.ReadFile(file));
    }
}