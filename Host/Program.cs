using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF_Service;
using WCF_Service.Services;

namespace Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            ServiceHost dbHost = null;
            ServiceHost customerHost = null;
            ServiceHost paymentBoxHost = null;
            ServiceHost movementHost = null;
            try
            {
                dbHost = new ServiceHost(typeof(DbService));
                dbHost.Open();
                Console.WriteLine("DBService is running... @ " + DateTime.Now.ToString());
                customerHost = new ServiceHost(typeof(CustomerService));
                customerHost.Open();
                Console.WriteLine("CustomerService is running... @ " + DateTime.Now.ToString());
                paymentBoxHost = new ServiceHost(typeof(PaymentBoxService));
                paymentBoxHost.Open();
                Console.WriteLine("PaymentBoxService is running... @ " + DateTime.Now.ToString());
                movementHost = new ServiceHost(typeof(MovementService));
                movementHost.Open();
                Console.WriteLine("MovementService is running... @ " + DateTime.Now.ToString());
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dbHost?.Close();
                customerHost?.Close();
                paymentBoxHost?.Close();
                movementHost?.Close();
            }

            
        }
    }
}
