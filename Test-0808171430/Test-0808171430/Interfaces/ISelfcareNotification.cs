using System;
using System.Collections.Generic;
using Test_0808171430.Models;

namespace Portal.SelfcareNotificationManager.Interface
{
    public interface ISelfcareNotification
    {
        //void CreateNotification(Customer customer, SelfcareNotification notification);
        void CreateNotification(Customer customer, string notification);
        IList<SelfcareNotification> Read(Customer customer);
        SelfcareNotification ReadByGuid(Customer customer, Guid guid);
        void Delete(string notification);
        //void Delete(SelfcareNotification notification);
    }
}
