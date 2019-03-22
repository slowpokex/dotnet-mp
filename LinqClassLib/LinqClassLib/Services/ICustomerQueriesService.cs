namespace Company.Services
{
    using System.Collections.Generic;

    public interface ICustomerQueriesService
    {
        IEnumerable<decimal> GetTotalOrderMoreThan(int? from);
    }
}