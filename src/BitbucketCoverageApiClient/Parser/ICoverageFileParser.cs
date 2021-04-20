using System.Collections.Generic;

namespace BitbucketCoverageApiClient.Parser
{
    internal interface ICoverageFileParser
    {
        List<FileCoverageModel> Parse(string coverageFile);
    }
}