

namespace CustomerClassLibTest
{
    using System;
    using System.Linq;
    using Company;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LinqSampleFirstTest
    {
        private DataSource _dataSource;

        [TestInitialize]
        public void Initialize()
        {
            _dataSource = new DataSource();
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Given
            // When
            // Then
            var customersList = _dataSource.Customers.Select(x => x.CustomerID);
        }
    }
}
