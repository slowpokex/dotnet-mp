namespace CustomerClassRunner
{
    using System;
    using Company.DataSources;
    using Company.Services;
    using CustomerClassLib.Utils;

    public class LinqSampleRunner
    {
        public static void Main(string[] args)
        {
            var dataSource = new DataSource();
            var queryService = new CustomerQueriesService(dataSource);

            UtilService.ShowResult(queryService.GetCustomersWithIncorrectLocationData());

            Console.ReadKey();
        }
    }
}
