using Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UnitTest.DbService;
using UnitTest.PaymentService;
using WCF_Service.Faults;

namespace UnitTest
{
    public class PaymentBoxService_Test
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
        public void Add_Payment_Box()
        {
            using (var client = new PaymentBoxServiceClient())
            {
                int count = client.SelectAll().Length;
                client.Add(new PaymentBox()
                {
                    Name = "Test",
                    IsActive = true
                });
                Assert.AreEqual(count + 1, client.SelectAll().Length);
            }
        }

        [Test]
        public void Add_Two_Payment_Boxes_With_Same_Name()
        {
            using (var client = new PaymentBoxServiceClient())
            {
                client.Add(new PaymentBox()
                {
                    Name = "Test",
                    IsActive = true
                });


                Assert.Throws<FaultException<SqlUniqueFault>>(() =>
                {
                    client.Add(new PaymentBox()
                    {
                        Name = "Test",
                        IsActive = true
                    });
                });


            }
        }

        [Test]
        public void Activate_Payment_Box()
        {
            using (var client = new PaymentBoxServiceClient())
            {
                client.Add(new PaymentBox()
                {
                    Name = "ActivateTest",
                    IsActive = false
                });

                var paymentBox = client.SelectAll().FirstOrDefault(x => x.Name == "ActivateTest");
                using (var activateService = new PaymentBoxServiceClient())
                {
                    activateService.Activate(paymentBox.Id);
                }

                Assert.IsTrue(client.SelectAll().FirstOrDefault(x => x.Name == "ActivateTest").IsActive);
            }
        }

        [Test]
        public void Deactivate_Payment_Box()
        {
            using (var client = new PaymentBoxServiceClient())
            {
                client.Add(new PaymentBox()
                {
                    Name = "DeactivateTest",
                    IsActive = true
                });

                var paymentBox = client.SelectAll().FirstOrDefault(x => x.Name == "DeactivateTest");
                using (var activateService = new PaymentBoxServiceClient())
                {
                    activateService.Deactivate(paymentBox.Id);
                }

                Assert.IsFalse(client.SelectAll().FirstOrDefault(x => x.Name == "DeactivateTest").IsActive);
            }
        }

        [Test]
        public void Activate_Payment_Box_That_Is_Already_Active()
        {
            PaymentBox paymentBox;
            using (var client = new PaymentBoxServiceClient())
            {
                client.Add(new PaymentBox()
                {
                    Name = "ActivateTest",
                    IsActive = true
                });

                paymentBox = client.SelectAll().FirstOrDefault(x => x.Name == "ActivateTest");

            }

            using (var activateService = new PaymentBoxServiceClient())
            {

                Assert.Throws<FaultException<SqlInvalidOperationException>>(() =>
                {
                    activateService.Activate(paymentBox.Id);
                });
            }
        }

        [Test]
        public void Deactivate_Payment_Box_That_Is_Already_Deactive()
        {
            PaymentBox paymentBox;
            using (var client = new PaymentBoxServiceClient())
            {
                client.Add(new PaymentBox()
                {
                    Name = "DeactivateTest",
                    IsActive = false
                });

                paymentBox = client.SelectAll().FirstOrDefault(x => x.Name == "DeactivateTest");

            }

            using (var activateService = new PaymentBoxServiceClient())
            {

                Assert.Throws<FaultException<SqlInvalidOperationException>>(() =>
                {
                    activateService.Deactivate(paymentBox.Id);
                });
            }
        }

    }
}
