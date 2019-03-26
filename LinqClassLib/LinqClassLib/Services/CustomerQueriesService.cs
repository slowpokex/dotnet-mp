namespace Company.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Company.DataSources;

    public class CustomerQueriesService : ICustomerQueriesService
    {
        private readonly DataSource _dataSource;

        public CustomerQueriesService(DataSource dataSource)
        {
            if (dataSource == null)
            {
                throw new ArgumentNullException(nameof(dataSource));
            }
            _dataSource = dataSource;
        }

        public IEnumerable<object> GetTotalOrderMoreThan(int? from)
        {
            return _dataSource.Customers
                .Select(cust => new { Name = cust.CompanyName, Sum = cust.Orders.Select(order => order.Total).Sum() })
                .Where(cust => cust.Sum > from);
        }

        public IEnumerable<object> GetCustomerSupplierListWithGrouping()
        {
            return _dataSource.Customers
                .Select(cust => new { Name = cust.CompanyName, Sum = cust.Orders.Select(order => order.Total).Sum() });
        }

        public IEnumerable<object> GetCustomerSupplierList()
        {
            return _dataSource.Customers
                .Select(cust => new { Name = cust.CompanyName, Sum = cust.Orders.Select(order => order.Total).Sum() });
        }

        public IEnumerable<object> GetCustomerListWithMoreSum(int? from)
        {
            return new object[] { };
        }
    }
}
