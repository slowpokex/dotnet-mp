namespace Company.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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
                .Select(cust => new {
                    Name = cust.CompanyName,
                    Sum = cust.Orders
                        .Select(order => order.Total)
                        .Sum()
                })
                .Where(cust => cust.Sum > from);
        }

        public IEnumerable<object> GetCustomerSupplierList()
        {
            return _dataSource.Customers
                .Select(cust => new {
                    Name = cust.CompanyName,
                    Customers = _dataSource.Suppliers
                    .Where(s => s.Country == cust.Country && s.City == cust.City)
                    .Select(s => s.SupplierName)
                    .Aggregate(new StringBuilder(), (ag, n) => ag.Append(n).Append(", "))
                });
        }

        public IEnumerable<object> GetCustomerSupplierListWithGrouping()
        {
            return _dataSource.Customers
                .GroupJoin(_dataSource.Suppliers, c => c.City, s => s.City, (c, s) => new {
                    Customer = c.CompanyName,
                    Suppliers = s.Select(sn => sn.SupplierName)
                                 .Aggregate(new StringBuilder(), (ag, n) => ag.Append(n).Append(", "))
                });
        }

        public IEnumerable<object> GetCustomersWithOrderMoreThan(int? from)
        {
            return _dataSource.Customers
                .Where(cust => cust.Orders.Any(o => o.Total >= from))
                .Select(cust => new {
                    Name = cust.CompanyName,
                    TotalOrders = string.Join(", ", cust.Orders.Select(o => o.Total))
                });
        }

        public IEnumerable<object> GetCustomersFrom(DateTime? date)
        {
            return GetCustomersFromPure(date)
                .Select(x => new {
                    Name = x.CompanyName,
                    FirstPurchaseDate = GetFirstOrderDate(x)
                });
        }

        private IEnumerable<Customer> GetCustomersFromPure(DateTime? date)
        {
            return _dataSource.Customers
                .Where(x => CheckDateIsGreatFrom(x, date));
        }

        private DateTime? GetFirstOrderDate(Customer cust)
        {
            var firstOrderDate = cust.Orders
                    .OrderBy(d => d.OrderDate)
                    .FirstOrDefault();

            return firstOrderDate?.OrderDate;
        }

        private bool CheckDateIsGreatFrom(Customer cust, DateTime? date)
        {
            return Nullable.Compare(GetFirstOrderDate(cust), date) > 0;
        }

        public IEnumerable<object> GetCustomersFromWithOrdering(DateTime? date)
        {
            return GetCustomersFromPure(date)
                .OrderBy(x => x.Orders.Select(y => y.OrderDate).LastOrDefault())
                .ThenBy(x => x.Orders.Select(y => y.Total).Sum())
                .ThenBy(x => x. CompanyName)
                .Select(x => new {
                    Name = x.CompanyName,
                    LastOrder = x.Orders.Select(y => y.OrderDate).LastOrDefault(),
                    TotalSum = x.Orders.Select(y => y.Total).Sum()
                });
        }

        public IEnumerable<object> GetCustomersWithIncorrectLocationData()
        {
            return _dataSource.Customers
                .Where(IsEmptyOrIncorrectCustomerLocationData)
                .Select(cust => new {
                    Name = cust.CompanyName,
                    Region = cust.Region,
                    postalCode = cust.PostalCode,
                    Phone = cust.Phone
                });
        }

        public IEnumerable<object> GroupProductsByCategory()
        {
            return _dataSource.Products
                .GroupBy(x => x.Category)
                .Select(x => new {
                    Name = x.Key,
                    Values = x.GroupBy(y => y.UnitsInStock)
                                .Select(z => new {
                                    HasOnStock = z.Key,
                                    Products = z.OrderBy(a => a.UnitPrice)
                                })
                });
        }

        public IEnumerable<object> GroupProductsByThresholds()
        {
            return _dataSource.Products;
        }

        public IEnumerable<object> AverageTotalByCities()
        {
            return _dataSource.Customers
                .GroupBy(cust => cust.City)
                .Select(c => new {
                    Name = c.Key,
                    AvgTotal = c.SelectMany(x => x.Orders.Select(o => o.Total)).Average(),
                    AvgCount = c.Select(x => x.Orders.Length).Average()
                });
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
