using Newtonsoft.Json;

namespace BitbucketCoverageApiClient.Bitbucket
{
    public class FileCoverageInfo
    {
        [JsonProperty("path")] public string Path { get; set; }

        [JsonProperty("coverage")] public string Coverage { get; set; }
    }
}