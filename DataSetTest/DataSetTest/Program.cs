using DataSetTest.Data;
using DataSetTest.Models;
using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.IO;

namespace DataSetTest
{
    class Program
    {
        static void Main(string[] args)
        {
            LocalReport localReport = new LocalReport();
            DsCustomer dsCustomer = new DsCustomer();

            var customer = new Customer();

            var table = new DataTable("Customer");

            table.Columns.Add("FullName", typeof(string));
            table.Columns.Add("Age", typeof(int));
            table.Columns.Add("Phone", typeof(string));

            table.Rows.Add(
                customer.FullName,
                customer.Age,
                customer.Phone
            );

            dsCustomer.Tables.Add(table);

            var source = new ReportDataSource("DsCustomer", table);

            localReport.ReportPath = @"c:\users\nikif\documents\visual studio 2017\Projects\DataSetTest\DataSetTest\Data\Report.rdlc";            
            localReport.DataSources.Clear();
            localReport.DataSources.Add(source);


            try
            {
                byte[] wordDoc = localReport.Render("WORD");

                using (var stream = new FileStream(@"C:\Test Dev\Output\statment-test.doc", FileMode.Create, FileAccess.Write))
                {
                    stream.Write(wordDoc, 0, wordDoc.Length);
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }

            Console.ReadKey();
        }
    }
}
