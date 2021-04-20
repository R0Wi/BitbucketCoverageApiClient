using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BitbucketCoverageApiClient.Bitbucket;
using BitbucketCoverageApiClient.Parser;

namespace BitbucketCoverageApiClient
{
    internal class CoverageFileReader : ICoverageFileReader
    {
        private readonly ParserFactory _parserFactory = new ParserFactory();

        public Files ReadFile(CoverageFile file) => ReadFiles(new CoverageFile[] { file }).First();

        public IEnumerable<Files> ReadFiles(IEnumerable<CoverageFile> files) => files.Select(file =>
        {
            Check(file);

            var parser = _parserFactory.CreateParser(file.FileFormat);
            var result = parser.Parse(file.FilePath);
            var bitbucketModel = new Files
            {
                FilesList = result.Select(res => new FileCoverageInfo
                {
                    Path = GetPath(res),
                    Coverage = GetCoverage(res)
                }).ToList()
            };

            return bitbucketModel;
        });

        private static void Check(CoverageFile file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file), "Coverage file cannot be null");
            if (string.IsNullOrEmpty(file.FilePath))
                throw new ArgumentNullException(nameof(file.FilePath), "Coverage file path cannot be empty or null");
            if (!File.Exists(file.FilePath))
                throw new FileNotFoundException("Coverage file was not found");
        }

        private static string GetCoverage(FileCoverageModel model)
        {
            var strings = new[]
            {
                $"C:{string.Join(',', model.CoveredLines)}",
                $"P:{string.Join(',', model.PartlyCoveredLines)}",
                $"U:{string.Join(',', model.UncoveredLines)}"
            };

            return string.Join(';', strings);
        }

        private static string GetPath(FileCoverageModel model)
        {
            return model.RelativeFilename?.Replace(@"\", "/");
        }
    }
}