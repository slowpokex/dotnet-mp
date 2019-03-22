namespace CustomerClassRunner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Company.DataSources;
    using Company.Services;

    class LinqSampleRunner
    {
        static void Main(string[] args)
        {
            var dataSource = new DataSource();
            var queryService = new CustomerQueriesService(dataSource);

            ShowResult(queryService.GetTotalOrderMoreThan(10000));

            Console.ReadKey();
        }

        private static void ShowResult<T>(IEnumerable<T> results)
        {
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }
    }
}
