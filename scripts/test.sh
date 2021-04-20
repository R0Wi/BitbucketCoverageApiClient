#!/bin/bash

SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"

dotnet test -c Release --logger "trx;LogFileName=BitbucketCoverageApiClient.testresults.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura ${SCRIPT_DIR}/../tests/BitbucketCoverageApiClient.Test/BitbucketCoverageApiClient.Test.csproj
dotnet test -c Release --logger "trx;LogFileName=BitbucketCoverageApiClient.Console.testresults.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura ${SCRIPT_DIR}/../tests/BitbucketCoverageApiClient.Commandline.Test/BitbucketCoverageApiClient.Commandline.Test.csproj
  