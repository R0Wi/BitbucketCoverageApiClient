using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

namespace BitbucketCoverageApiClient.Bitbucket
{
    internal class Client : IClient
    {
        private const string PATH = "rest/code-coverage/1.0/commits";
        private readonly IRestClient _client;

        public Client(string baseUrl, string username, string password) : this(new RestClient(baseUrl), new HttpBasicAuthenticator(username, password))
        {

        }

        internal Client(IRestClient restClient, IAuthenticator authenticator)
        {
            _client = restClient;
            _client.UseNewtonsoftJson();
            _client.Authenticator = authenticator;
        }

        public List<FileCoverageInfo> Upload(string commit, Files coverage)
        {
            var request = new RestRequest($"{PATH}/{commit}", Method.POST);
            request.AddJsonBody(coverage);
            var response = _client.Execute<List<FileCoverageInfo>>(request);

            if (!response.IsSuccessful)
                throw new RequestException(
                    @$"Error while trying to execute request.{Environment.NewLine}Statuscode: {response.StatusCode}{Environment.NewLine}ServerResponse: {response.Content}{Environment.NewLine}ErrorMessage: {response.ErrorMessage}",
                    response.ErrorException);

            return response.Data;
        }
    }
}