using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using BitbucketCoverageApiClient.Parser.Cobertura.Model;

namespace BitbucketCoverageApiClient.Parser.Cobertura
{
    internal class CoberturaParser : ICoverageFileParser
    {
        public List<FileCoverageModel> Parse(string coverageFile)
        {
            var serializer = new XmlSerializer(typeof(CoverageReport));
            using (var reader = XmlReader.Create(coverageFile))
            {
                var report = (CoverageReport) serializer.Deserialize(reader);
                return Parse(report);
            }
        }

        private static List<FileCoverageModel> Parse(CoverageReport report)
        {
            return report.Packages.Package
                .SelectMany(package => package.Classes.Class)
                .Select(c => new FileCoverageModel
                {
                    RelativeFilename = c.Filename,
                    UncoveredLines = c.Lines.Line
                        .Where(l => l.Hits == 0)
                        .Select(l => l.Number)
                        .ToList(),
                    CoveredLines = c.Lines.Line
                        .Where(l => l.Hits > 0 ||
                                    l.ConditionCoverage?.ToLower() == "true" && l.Branch.StartsWith("100"))
                        .Select(l => l.Number)
                        .ToList(),
                    PartlyCoveredLines = c.Lines.Line
                        .Where(l => l.ConditionCoverage?.ToLower() == "true" && !l.Branch.StartsWith("100"))
                        .Select(l => l.Number)
                        .ToList()
                })
                .ToList();
        }
    }
}