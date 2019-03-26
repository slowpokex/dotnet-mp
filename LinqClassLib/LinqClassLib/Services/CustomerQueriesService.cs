namespace Company.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Company.Data;
    using Company.DataSources;
    using CustomerClassLib.Utils;

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

        public IEnumerable<object> GetCustomersWithIncorrectLocationData()
        {
            return _dataSource.Customers
                .Where(IsEmptyOrIncorrectCustomerLocationData)
                .Select(cust => new { Name = cust.CompanyName, Region = cust.Region, postalCode = cust.PostalCode, Phone = cust.Phone });
        }

        private bool IsEmptyOrIncorrectCustomerLocationData(Customer cust)
        {
            return string.IsNullOrEmpty(cust.Region)
                || string.IsNullOrEmpty(cust.PostalCode)
                || ( !string.IsNullOrEmpty(cust.PostalCode) && !cust.PostalCode.All(char.IsDigit) )
                || string.IsNullOrEmpty(cust.Phone)
                || ( !string.IsNullOrEmpty(cust.Phone) && !UtilService.CheckWellBracketsFormed(cust.Phone) );
        }
    }
}
