namespace ReportServiceTest
{
    using System;
    using System.IO;
    using System.Data;
    using ReportServiceTest.Data;
    using ReportServiceTest.Models;
    using Microsoft.Reporting.WinForms;

    public class Program
    {
        private static LocalReport _report;
        private static DsStatment _localDs;

        static void Main(string[] args)
        {
            // Initilalization of a new instance OsagoPolicy class
            var policy = new OsagoPolicy();

            _report = new LocalReport();
            _report.ReportPath = @"C:\Users\nikif\Documents\visual studio 2017\Projects\ReportServiceTest\ReportServiceTest\Data\Statment.rdlc";
            // Initilalization of a new instance DsStatment local DataSet
            _localDs = new DsStatment();

            DataTable osagoTable = policy.GetData_OsagoPolicyRouting();
            // Make an attempt to fill the DataTable
            try
            {
                // Fill OSAGOPolicy DataTable
                _report.InitialReport_LocalReportRouting(osagoTable, _localDs);                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            //  Make an attempt to fill the Source DataSet of RDLC Reporting
            try
            {
                _report.WriteToWordDoc_LocalReportRouting();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            // Clear the all of records in the Source DataSet
            _report.DataSources.Clear();

            Console.ReadKey();
        }      
    }

    public static class Routings
    {
        public static DataTable GetData_OsagoPolicyRouting(this OsagoPolicy _this)
        {
            // Create Table
            var dataTable = new DataTable("OsagoPolicy");
            // Create Columns
            dataTable.Columns.Add("CompanyName", typeof(String));
            dataTable.Columns.Add("BeginInsurer", typeof(DateTime));
            dataTable.Columns.Add("EndInsurer", typeof(DateTime));

            // Fill Columns/Add Records
            dataTable.Rows.Add(
                _this.CompanyName,                
                _this.BeginInsurer,
                _this.EndInsurer);

            return dataTable;
        }

        public static void InitialReport_LocalReportRouting(this LocalReport _this, DataTable table, DsStatment _localDs)
        {
            try
            {
                // Initilaze the Local DataSet
                if (_localDs == null)
                {
                    _localDs = new DsStatment();
                }

                if (string.IsNullOrWhiteSpace(_this.ReportPath))
                {
                    throw new DirectoryNotFoundException();
                }

                // merge data with "Data()" method and MyDataSet Table :
                _localDs.Tables[table.TableName].Merge(table);

                var rds = new ReportDataSource("DsStatment", table);

                _this.DataSources.Add(rds);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public static void WriteToWordDoc_LocalReportRouting(this LocalReport _this)
        {
            Byte[] wordDoc_bytes = _this.Render(
                "WORD",
                null,
                out string mimeType,
                out string encoding,
                out string extension,
                out string[] streamids,
                out Warning[] warnings);

            using (FileStream fs = new FileStream($@"C:\Test Dev\Output\{DateTime.Now}_statment.doc", FileMode.Create))
            {
                fs.Write(wordDoc_bytes, 0, wordDoc_bytes.Length);
            }                      
        }
    }
}
