using System;
using BitbucketCoverageApiClient.Parser.Cobertura;

namespace BitbucketCoverageApiClient.Parser
{
    internal class ParserFactory
    {
        public ICoverageFileParser CreateParser(CoverageFileFormat format)
        {
            switch (format)
            {
                case CoverageFileFormat.Cobertura:
                    return new CoberturaParser();
                default:
                    throw new NotSupportedException(nameof(format));
            }
        }
    }
}