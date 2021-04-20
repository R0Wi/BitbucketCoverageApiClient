using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

namespace BitbucketCoverageApiClient.Bitbucket
{
    internal interface IClient
    {
        List<FileCoverageInfo> Upload(string commit, Files coverage);
    }
}