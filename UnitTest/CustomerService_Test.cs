using Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.CustomerService;
using UnitTest.DbService;

namespace UnitTest
{
    public class CustomerService_Test
    {
        [SetUp]
        public void Setup()
        {
            using (DbServiceClient client = new DbServiceClient())
            {
                client.ResetDatabase();
            }
        }

        [Test]
        public void Add_Customer()
        {
            using (var client = new CustomerServiceClient())
            {
                int count = client.SelectAll().Length;
                client.Add(new Customer()
                {
                    Name = "Test",
                    LastName = null,
                    RegistrationDate = DateTime.Now
                });
                Assert.AreEqual(count + 1, client.SelectAll().Length);
            }
        }

        [Test]
        public void Select_All_Unattended_Customers()
        {
            using (var client = new CustomerServiceClient())
            {
                var result = client.SelectAllUnattendedCustomers();

                Assert.IsNotEmpty(result);
            }
        }
    }
}
