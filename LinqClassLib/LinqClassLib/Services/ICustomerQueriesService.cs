namespace Company.Services
{
    using System;
    using System.Collections.Generic;

    public interface ICustomerQueriesService
    {
        IEnumerable<object> GetTotalOrderMoreThan(int? from);

        IEnumerable<object> GetCustomerSupplierList();

        IEnumerable<object> GetCustomerSupplierListWithGrouping();

        IEnumerable<object> GetCustomersWithOrderMoreThan(int? from);

        IEnumerable<object> GetCustomersFrom(DateTime? date);

        IEnumerable<object> GetCustomersFromWithOrdering(DateTime? date);

        IEnumerable<object> GetCustomersWithIncorrectLocationData();

        IEnumerable<object> GroupProductsByCategory();

        IEnumerable<object> GroupProductsByThresholds();

        IEnumerable<object> AverageTotalByCities();
    }
}