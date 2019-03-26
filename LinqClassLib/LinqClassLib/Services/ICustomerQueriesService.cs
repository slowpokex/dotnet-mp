namespace Company.Services
{
    using System.Collections.Generic;

    public interface ICustomerQueriesService
    {
        IEnumerable<object> GetTotalOrderMoreThan(int? from);
        IEnumerable<object> GetCustomerSupplierList();
        IEnumerable<object> GetCustomerSupplierListWithGrouping();
        IEnumerable<object> GetCustomersWithIncorrectLocationData();
    }
}