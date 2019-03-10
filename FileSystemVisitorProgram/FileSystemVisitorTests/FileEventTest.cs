namespace FileSystemVisitorTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FileSystemVisitorLib;
    using Moq;

    [TestClass]
    public class FileEventTest
    {
        [TestMethod]
        public void CanTest()
        {
            // Arrange
            var mock = new Mock<FileSystemVisitor>();
            mock.Setup(a => a.GetAllFilesFromDir("D:/Projects/")).Returns(new string[] {".git", "index.html"});
            
        }
    }
}
