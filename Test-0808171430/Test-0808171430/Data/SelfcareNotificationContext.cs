using Portal.SelfcareNotificationManager;
using System.Data.Entity;
using Test_0808171430.Models;

namespace Test_0808171430.Data
{
    public class SelfcareNotificationContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SelfcareNotification> SelfcareNotifications { get; set; }

        public SelfcareNotificationContext()
            : base() 
        {

        }
    }
}
