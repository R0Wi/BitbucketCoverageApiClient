using System.Collections.Generic;
using BitbucketCoverageApiClient.Bitbucket;

namespace BitbucketCoverageApiClient
{
    internal interface ICoverageFileReader
    {
        Files ReadFile(CoverageFile file);
        IEnumerable<Files> ReadFiles(IEnumerable<CoverageFile> files);
    }
}