namespace CustomerClassRunner
{
    using System;
    using System.Collections.Generic;
    using Company.DataSources;
    using Company.Services;
    using CustomerClassLib.Utils;

    public class LinqSamples
    {
        public static void Main(string[] args)
        {
            var dataSource = new DataSource();
            var queryService = new CustomerQueriesService(dataSource);

            IEnumerable<object> result = null;

            // 1
            // result = queryService.GetTotalOrderMoreThan(10000);

            // 2
            // result = queryService.GetCustomerSupplierList();

            // 2 with grouping
            // result = queryService.GetCustomerSupplierListWithGrouping();

            // 3
            // result = queryService.GetTotalOrderMoreThan(1000);

            // 4
            // result = queryService.GetCustomersFrom(new DateTime(1997, 7, 20));

            // 5
            // result = queryService.GetCustomersFromWithOrdering(new DateTime(1996, 7, 20));

            // 6
            // result = queryService.GetCustomersWithIncorrectLocationData();

            // 7
            // result = queryService.GroupProductsByCategory();

            // 8
            result = queryService.GroupProductsByThresholds();

            // 9
            // result = queryService.AverageTotalByCities()

            UtilService.ShowResult(result);

            Console.ReadKey();
        }
    }
}
