namespace CustomerClassRunner
{
    using System;
    using Company.DataSources;
    using Company.Services;
    using CustomerClassLib.Utils;

    public class LinqSample
    {
        public static void Main(string[] args)
        {
            var dataSource = new DataSource();
            var queryService = new CustomerQueriesService(dataSource);

            // 1
            // UtilService.ShowResult(queryService.GetTotalOrderMoreThan(10000));

            // 2
            // UtilService.ShowResult(queryService.GetCustomerSupplierList());

            // 2 with grouping
            // UtilService.ShowResult(queryService.GetCustomerSupplierListWithGrouping());

            // 3
            // UtilService.ShowResult(queryService.GetTotalOrderMoreThan(1000));

            // 4
            UtilService.ShowResult(queryService.GetCustomersFrom(new DateTime(2015, 7, 20)));

            // 6
            // UtilService.ShowResult(queryService.GetCustomersWithIncorrectLocationData());

            // 9
            // UtilService.ShowResult(queryService.AverageTotalByCities());

            Console.ReadKey();
        }
    }
}
