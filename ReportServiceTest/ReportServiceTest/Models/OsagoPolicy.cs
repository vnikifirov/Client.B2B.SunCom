using System;

namespace ReportServiceTest.Models
{
    public class OsagoPolicy
    {
        public string CompanyName { get; set; }
        public DateTime BeginInsurer { get; set; }
        public DateTime EndInsurer { get; set; }

        public OsagoPolicy()
        {
            CompanyName = "ООО Чулпан";
            BeginInsurer = DateTime.Now;
            EndInsurer = DateTime.Now.AddYears(1).AddDays(-1);
        }
    }
}
