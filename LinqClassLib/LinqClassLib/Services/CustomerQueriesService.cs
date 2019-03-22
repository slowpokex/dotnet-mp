namespace Company.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Company.DataSources;

    public class CustomerQueriesService : ICustomerQueriesService
    {
        private readonly DataSource _dataSource;

        public CustomerQueriesService(DataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public IEnumerable<decimal> GetTotalOrderMoreThan(int? from)
        {
            return _dataSource.Customers
                .Select(cust => cust.Orders.Select(order => order.Total).Sum())
                .Where(sum => sum > from);
        }

        public IEnumerable<decimal> GetSupplierList(int? from)
        {
            return _dataSource.Customers
                .Select(cust => cust.Orders.Select(order => order.Total).Sum())
                .Where(sum => sum > from);
        }
    }
}
