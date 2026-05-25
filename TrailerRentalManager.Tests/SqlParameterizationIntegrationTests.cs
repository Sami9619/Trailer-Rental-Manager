using System;
using System.Data;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trailer_Rental_Manager.Operations;
using Trailer_Rental_Manager.Repositories;

namespace TrailerRentalManager.Tests
{
    [TestClass]
    public class SqlParameterizationIntegrationTests
    {
        [TestMethod]
        public void CustomerInsert_ApostropheAndInjectionLikeText_PreservesTableAndRows()
        {
            string directory = TestDatabase.CreateTemporaryDirectory();
            string databasePath = Path.Combine(directory, "test.db");

            try
            {
                using (DBOperations.UseConnectionString(TestDatabase.CreateConnectionString(databasePath)))
                {
                    CustomerRepository.Insert("m", "Patrick", "O'Connor", null, null, null, null, null, null, null);
                    CustomerRepository.Insert("m", "Robert'); DROP TABLE Customer;--", "Tester", null, null, null, null, null, null, null);

                    object customerTableExists = DBOperations.ExecuteScalar(@"
                        SELECT COUNT(*)
                        FROM sqlite_master
                        WHERE type = 'table'
                          AND name = 'Customer';");

                    DataTable customers = CustomerRepository.GetExport();

                    Assert.AreEqual(1L, Convert.ToInt64(customerTableExists));
                    Assert.AreEqual(2, customers.Rows.Count);
                    Assert.IsTrue(ContainsCustomer(customers, "Patrick", "O'Connor"));
                    Assert.IsTrue(ContainsCustomer(customers, "Robert'); DROP TABLE Customer;--", "Tester"));
                }
            }
            finally
            {
                TestDatabase.DeleteDirectory(directory);
            }
        }

        private static bool ContainsCustomer(DataTable customers, string firstName, string lastName)
        {
            foreach (DataRow row in customers.Rows)
            {
                if (row["FirstName"].ToString() == firstName &&
                    row["LastName"].ToString() == lastName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
