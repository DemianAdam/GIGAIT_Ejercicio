using Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UnitTest.CustomerService;
using UnitTest.DbService;
using UnitTest.MovementService;
using UnitTest.PaymentService;


namespace UnitTest
{
    public class MovementService_Test
    {
        [SetUp]
        public void Setup()
        {
            using (var client = new DbServiceClient())
            {
                client.ResetDatabase();
            }
        }

        [Test]
        public void Select_All_Movements()
        {
            InstanceContext context = new InstanceContext(new MovementCallback());
            using (var client = new MovementServiceClient(context))
            {
                var result = client.SelectAll();

                Assert.IsNotEmpty(result);
            }
        }

        [Test]
        public void Add_Movement_With_Customer()
        {
            PaymentBox paymentBox;
            Customer customer;
            using (var client = new PaymentBoxServiceClient())
            {
                paymentBox = client.SelectById(1);
            }

            using (var client = new CustomerServiceClient())
            {
                customer = client.SelectAllUnattendedCustomers()[0];
            }

            InstanceContext context = new InstanceContext(new MovementCallback());
            using (var client = new MovementServiceClient(context))
            {
                int movements = client.SelectAll().Length;
                client.Add(new Movement()
                {
                    CreationDate = DateTime.Now,
                    OcurredDate = null,
                    PaymentBox = paymentBox,
                    Customer = customer
                });

                int updatedMovements = client.SelectAll().Length;

                Assert.AreEqual(movements + 1, updatedMovements);

            }
        }

        [Test]
        public void Add_Movement_Without_Customer()
        {
            PaymentBox paymentBox;
            using (var client = new PaymentBoxServiceClient())
            {
                paymentBox = client.SelectAll()[0];
            }

            InstanceContext context = new InstanceContext(new MovementCallback());
            using (var client = new MovementServiceClient(context))
            {
                int movements = client.SelectAll().Length;
                client.Add(new Movement()
                {
                    CreationDate = DateTime.Now,
                    OcurredDate = null,
                    PaymentBox = paymentBox,
                    Customer = null
                });

                int updatedMovements = client.SelectAll().Length;

                Assert.AreEqual(movements + 1, updatedMovements);

            }
        }
    }
}
