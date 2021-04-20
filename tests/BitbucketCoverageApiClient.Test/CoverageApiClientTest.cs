using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitbucketCoverageApiClient;
using BitbucketCoverageApiClient.Bitbucket;
using FakeItEasy;
using System.Collections.Generic;

namespace BitbucketCoverageApiClient.Test
{
    [TestClass]
    public class CoverageApiClientTest
    {
        private IClient _fakedClient;
        private ICoverageFileReader _fakedFileReader;
        private CoverageApiClient _testObject;

        [TestInitialize]
        public void Setup()
        {
            _fakedClient = A.Fake<IClient>();
            _fakedFileReader = A.Fake<ICoverageFileReader>();
            _testObject = new CoverageApiClient(_fakedClient, _fakedFileReader);
        }

        [TestMethod]
        public void TestReadAndUpload_SingleFile()
        {
            var files = new Files();
            A.CallTo(() => _fakedFileReader.ReadFile(A<CoverageFile>._))
                .Returns(files);
            
            _testObject.ReadAndUpload("myPath", CoverageFileFormat.Cobertura, "someHash");

            A.CallTo(() => _fakedFileReader.ReadFile(A<CoverageFile>.That.Matches(f => f.FileFormat == CoverageFileFormat.Cobertura && f.FilePath == "myPath")))
                .MustHaveHappenedOnceExactly()
                .Then(A.CallTo(() => _fakedClient.Upload("someHash", files))
                    .MustHaveHappenedOnceExactly());
        }

        [TestMethod]
        public void TestReadAndUpload_MultipleFiles()
        {
            var f1 = new Files();
            var f2 = new Files();
            var files = new Files[] { f1, f2 };
            A.CallTo(() => _fakedFileReader.ReadFiles(A<IEnumerable<CoverageFile>>._))
                .Returns(files);
            
            var cov1 = new CoverageFile();
            var cov2 = new CoverageFile();
            var covFiles = new CoverageFile[] { cov1, cov2 };
            
            _testObject.ReadAndUpload(covFiles, "someHash");

            A.CallTo(() => _fakedFileReader.ReadFiles(covFiles))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakedClient.Upload("someHash", f1))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakedClient.Upload("someHash", f2))
                .MustHaveHappenedOnceExactly();
        }
    }
}
