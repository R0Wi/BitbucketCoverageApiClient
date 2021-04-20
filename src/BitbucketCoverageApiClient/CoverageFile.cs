namespace BitbucketCoverageApiClient
{
    /// <summary>
    /// Represents a single coverage file.
    /// </summary>
    public class CoverageFile
    {
        /// <summary>
        /// Full path to the coverage file.
        /// </summary>
        public string FilePath { get; set; }
        
        /// <summary>
        /// Format of the coverage file.
        /// </summary>
        public CoverageFileFormat FileFormat { get; set; }
    }
}