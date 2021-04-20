using System.Collections.Generic;
using BitbucketCoverageApiClient.Bitbucket;

namespace BitbucketCoverageApiClient
{
    /// <summary>
    ///     Bitbucket client for reading and uploading coverage results from coverage files.
    /// </summary>
    public interface ICoverageApiClient
    {
        /// <summary>
        ///     Reads the coverage file and uploads the results to Bitbucket.
        /// </summary>
        /// <param name="coverageFilePath">Path to coverage file</param>
        /// <param name="format">Coverage file format</param>
        /// <param name="commitHash">Git commit hash to link the coverage to</param>
        /// <returns>API result from Bitbucket</returns>
        /// <exception cref="RequestException">Thrown if request to Bitbucket server failed</exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        /// <exception cref="System.UriFormatException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        List<FileCoverageInfo> ReadAndUpload(string coverageFilePath, CoverageFileFormat format, string commitHash);

        /// <summary>
        ///     Reads the coverage file and uploads the results to Bitbucket.
        /// </summary>
        /// <param name="files">The coverage files</param>
        /// <param name="commitHash">Git commit hash to link the coverage to</param>
        /// <returns>Multipe API results from Bitbucket. One per coverage file</returns>
        /// <exception cref="RequestException">Thrown if request to Bitbucket server failed</exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        /// <exception cref="System.UriFormatException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        List<List<FileCoverageInfo>> ReadAndUpload(IEnumerable<CoverageFile> files, string commitHash);
    }
}