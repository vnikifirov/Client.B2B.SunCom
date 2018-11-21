using System;

namespace Portal.SelfcareNotificationManager
{
    public class SelfcareNotification
    {
        public int SelfcareNotificationID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Created { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public  DateTime? Readed { get; set; }
        public Guid GUID { get; set; }
    }
}