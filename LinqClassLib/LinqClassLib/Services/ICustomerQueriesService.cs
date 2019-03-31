namespace Company.Services
{
    using System;
    using System.Collections.Generic;

    public interface ICustomerQueriesService
    {
        // 1
        IEnumerable<object> GetTotalOrderMoreThan(int? from);

        // 2
        IEnumerable<object> GetCustomerSupplierList();

        // 2 wit grouping
        IEnumerable<object> GetCustomerSupplierListWithGrouping();

        // 3
        IEnumerable<object> GetCustomersWithOrderMoreThan(int? from);

        // 4
        IEnumerable<object> GetCustomersFrom(DateTime? date);

        // 5
        IEnumerable<object> GetCustomersFromWithOrdering(DateTime? date);

        // 6
        IEnumerable<object> GetCustomersWithIncorrectLocationData();

        // 9
        IEnumerable<object> AverageTotalByCities();
    }
}