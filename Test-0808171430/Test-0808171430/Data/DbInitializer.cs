using Portal.SelfcareNotificationManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_0808171430.Models;

namespace Test_0808171430.Data
{
    public class DbInitializer
    {
        public static void Initialize(SelfcareNotificationContext context)
        {
            //context.Database.EnsureCreated();

            #region Add Customers in DataContext
            // Look for any students.
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }

            var customers = new Customer[]
            {
                new Customer{StubData="Carson"},
                new Customer{StubData="Meredith"},
                new Customer{StubData="Arturo"},
            };

            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }

            context.SaveChanges();
            #endregion

            #region Add Notifications in DataContext
            var notifications = new SelfcareNotification[]
                {
                new SelfcareNotification{CustomerID=1, Created = DateTime.Parse("2017-08-08"), Header="Header1", Text="TextText Text...", Readed=null, GUID=Guid.NewGuid()},
                new SelfcareNotification{CustomerID=2, Created = DateTime.Parse("2017-05-08"), Header="Header2", Text="Text Text Text.", Readed=DateTime.Parse("2017-05-08"), GUID=Guid.NewGuid()},
                new SelfcareNotification{CustomerID=3, Created = DateTime.Parse("2017-07-08"), Header="Header3", Text="Text TextTextText Text.", Readed=DateTime.Parse("2017-08-08"), GUID=Guid.NewGuid()}
                };

            foreach (SelfcareNotification n in notifications)
            {
                context.SelfcareNotifications.Add(n);
            }

            context.SaveChanges(); 
            #endregion
        }
    }
}
