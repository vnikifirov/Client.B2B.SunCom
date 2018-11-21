using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Test_170807
{
    public class Program
    {
        static void Main(string[] args)
        {
            const string innerXml = @"C:\TMP\innerDoc.xml";
            const string ex_file = @"C:\TMP\expected_file.gz";
            const string ac_file = @"C:\TMP\actual_file.gz";
            const string srcDoc = @"C:\TMP\srcDoc.xml";

            // Get bytes Expected XML Docuemnt
            byte[] innerDoc_bytes = File.ReadAllBytes(innerXml);

            // Coppressing Source XML Docuemnt
            GZipCompression(ex_file, innerDoc_bytes);

            // Get XML Doc from Respounced
            XDocument xDoc = XMLProvider.GetDoc(srcDoc);
            // Receive Zip Archive Data from Respounced
            string data = XMLProvider.GetData(xDoc, "zipEncodedData")?.Value;

            // Convert to byte array Received Zip Archive
            byte[] ac_bytes = Encoding.ASCII.GetBytes(data);
            // Receive byte array from Source GZip Archive
            byte[] ex_bytes = File.ReadAllBytes(ex_file);

            if (ac_bytes.Length != ex_bytes.Length)
                Console.WriteLine("Length array is not some");

            // Get Actual String from Respounced Byte Array
            string actualString = Encoding.UTF8.GetString(ac_bytes);
            // Get Expected String from Source GZip Archive



            //string expectedString = GZipDecompression(ex_file, ex_bytes);

            //if (actualString.Length != expectedString.Length)
            //    Console.WriteLine("Length XML Doc is not some");

            string ac_hexCode = GetHexCode(ac_bytes);
            string ex_hexCode = GetHexCode(ex_bytes);

            Console.WriteLine("Signature:");
            Console.WriteLine(ac_hexCode);
            Console.WriteLine(ex_hexCode);

            Console.ReadKey();
        }

        private static string GetHexCode(byte[] ac_bytes)
        {
            byte[] data = new byte[16];

            Array.Copy(ac_bytes, 0x0, data, 0, 0x10);

            string signature = BitConverter.ToString(data);

            return signature;
        }

        public static void GZipCompression(string fileName, byte[] file)
        {
            using (var filestream = new FileStream(fileName, FileMode.Create))
            using (var gs = new GZipStream(filestream, CompressionMode.Compress))
            {
                if (gs.CanWrite)
                {
                    gs.Write(file, 0, file.Length);
                }
            }
        }

        public static string GZipDecompression(string fileName, byte[] file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(file, 0, file.Length);
                ms.Position = 0;

                byte[] buffer = new byte[file.Length];
                using (var zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    zip.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }
    }

    internal class XMLProvider
    {
        internal static XElement GetData(XDocument xDoc, string localName)
        {            
            return xDoc.Descendants().Where(x => x.Name.LocalName == localName).FirstOrDefault();
        }

        internal static XDocument GetDoc(string fileName)
        {            
            return XDocument.Load(File.Open(fileName, FileMode.Open, FileAccess.Read));
        }
    }
}
