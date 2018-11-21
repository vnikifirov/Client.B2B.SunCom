using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System.Text.RegularExpressions;

namespace AngleSharpHtml
{
    class Program
    {
        static void Main(string[] args)
        {
            GetList();

            Console.WriteLine("Press any key to continue");
        }

        private static string GetList()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Загрузка списка террористов. Инициализация загрузки");

            var address = @"http://fedsfm.ru/documents/terrorists-catalog-portal-act";
            var parser = new HtmlParser();

            string htmlString = string.Empty;
            using (var client = new WebClient { Encoding = Encoding.UTF8 })
            {
                htmlString = client.DownloadString(address);
            }

            // If HTML document was not uploaded
            if (string.IsNullOrWhiteSpace(htmlString))
            {
                Console.WriteLine("Загрузка списка террористов. Загрузка не удалась, файл не найден.");
                return string.Empty;
            }
            // Parse string to html nodes
            IHtmlDocument document = parser.Parse(htmlString);            
            // Gets div cointainer by Id
            IElement div = document.GetElementById("textRussianFL");            
            // If unable to find elemnet by id
            if (div == null)
            {
                Console.WriteLine("Загрузка списка террористов. Загрузка не удалась, список не найден.");
                return string.Empty;
            }

            // Trying to get all of text from childs paragraph in the div
            IEnumerable<string> textContent = null;
            try
            {                
                textContent = div.ChildNodes.OfType<IHtmlParagraphElement>().Select(p => p.TextContent).DefaultIfEmpty();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Загрузка списка террористов. Загрузка не удалась, произошла  ошибка. {Environment.NewLine}{ex.ToString()}");                
            }

            // if unable to find paragraphs that have contained text
            if (textContent == null)
            {
                Console.WriteLine("Загрузка списка террористов. Загрузка не удалась, список не найден.");
                return string.Empty;
            }
            
            // Create a new blank for invalid strings
            var invalidStrings = new List<string>();

            // Create a new blank for valid strings
            var validStrings = new List<string>();                        
            
            // Regular expression what's matching First, Last, Middle names and DateTime from input string            
            var regex = new Regex(@"^[\d]+[.][ ]+(?<FirstName>[а-яёА-ЯЁ\-]+)\s(?<LastName>[а-яёА-ЯЁ\-]+)\s(?<MiddleName>[а-яёА-ЯЁ\- ]+)[*]?[,].*(?<DateTime>\d{2}.\d{2}.\d{2,4})",
                                RegexOptions.None);
            
            foreach (string stringContent in textContent)
            {
                string parsedString = string.Empty;
                Match match = null;

                // Trying to find a suitable records
                try
                {
                    match = regex.Match(stringContent);
                    if (!match.Success)
                    {
                        invalidStrings.Add(stringContent);
                        Console.WriteLine($"Загрузка списка террористов. Запись имеет некорректный формат. {stringContent}");
                        continue;
                    }
                    
                    // Gets data from founded cases
                    string firstName = match.Groups["FirstName"].Value;
                    string lastName = match.Groups["LastName"].Value;
                    string middleName = match.Groups["MiddleName"].Value;                
                    string dateTime = match.Groups["DateTime"].Value;
                                                            
                    parsedString = string.Join(",", firstName, lastName, middleName, dateTime);
                    // Add to list the valid string
                    validStrings.Add(parsedString);
                }
                catch (Exception ex)
                {
                    invalidStrings.Add(string.Join(" ",
                        "Загрузка списка террористов. Во время извлечения:",
                        stringContent, "Произошла ошибка:",
                        Environment.NewLine,
                        ex.ToString()));
                }
            }     

            string _destinationCSVPath = @"C:\Test Dev\Output\";
            string fileName = Path.Combine(_destinationCSVPath, $"{DateTime.Now.ToString("yyyyMMddHHmmss")}_valid_records_created.csv");
            using (var sw = new StreamWriter(File.Open(fileName, FileMode.Create), Encoding.UTF8))
            {
                validStrings.ForEach(s => sw.WriteLine(s));                
            }

            string invalidRecords = Path.Combine(_destinationCSVPath, $"{DateTime.Now.ToString("yyyyMMddHHmmss")}_invalid_records_created.csv");
            using (var sw = new StreamWriter(File.Open(invalidRecords, FileMode.Create), Encoding.UTF8))
            {
                invalidStrings.ForEach(s => sw.WriteLine(s));
            }

            Console.WriteLine($"Загрузка списка террористов. файл записан: {fileName}");
            Console.WriteLine($"Загрузка списка террористов. Количество корректных строк: {validStrings.Count}");
            Console.WriteLine($"Загрузка списка террористов. Количество некорректных строк: {invalidStrings.Count}");           

            foreach (string invalidString in invalidStrings)
                Console.WriteLine(invalidString);

            Console.WriteLine("Загрузка списка террористов. Загрузка завершена");            

            return fileName;
        }   
    }
}
