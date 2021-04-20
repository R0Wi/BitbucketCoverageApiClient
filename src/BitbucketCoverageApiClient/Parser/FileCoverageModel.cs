using System.Collections.Generic;

namespace BitbucketCoverageApiClient.Parser
{
    internal class FileCoverageModel
    {
        public string RelativeFilename { get; set; }
        public List<int> CoveredLines { get; set; } = new List<int>();
        public List<int> PartlyCoveredLines { get; set; } = new List<int>();
        public List<int> UncoveredLines { get; set; } = new List<int>();
    }
}