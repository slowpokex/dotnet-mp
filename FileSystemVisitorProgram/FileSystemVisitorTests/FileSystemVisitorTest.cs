namespace FileSystemVisitorTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FileSystemVisitorLib;
    using Moq;
    using FileSystemVisitorLib.FileEventObservers;
    using System.IO;

    [TestClass]
    public class FileSystemVisitorTest
    {
        private Mock<IEventObservable> _eventObservableMock;

        [TestInitialize]
        public void Initialize()
        {
            _eventObservableMock = new Mock<IEventObservable>();
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ShouldFailIfPassBadPath()
        {
            // Arrange, Act
            var visitor = new FileSystemVisitor(null, _eventObservableMock.Object);

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void ShouldGetDefiniteObservable()
        {
            // Arrange, Act
            var visitor = new FileSystemVisitor("D:/Mentoring", _eventObservableMock.Object);

            // Assert
            Assert.AreEqual(visitor.FileSystemEventObservable, _eventObservableMock.Object);
        }
    }
}
