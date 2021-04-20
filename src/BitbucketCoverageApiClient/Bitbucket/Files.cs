using System.Collections.Generic;
using Newtonsoft.Json;

namespace BitbucketCoverageApiClient.Bitbucket
{
    internal class Files
    {
        [JsonProperty("files")] public List<FileCoverageInfo> FilesList { get; set; } = new List<FileCoverageInfo>();
    }
}