using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System;

namespace Test_1708071632
{
    class Program
    {
        public static string FileName { get; set; } = "data.bin";
        public static List<Mock> ListMocks { get; set; }

        static void Main(string[] args)
        {
            ListMocks = new List<Mock>
            {
                new Mock { Id = 1, Name = "Test1" },
                new Mock { Id = 2, Name = "Test2" },
                new Mock { Id = 3, Name = "Test3" }
            };

            System.Console.WriteLine($"Step 1: Quantity Mocks - {ListMocks.Count}");

            SerializeRestrictedUsers();

            // Remove all Records 
            ListMocks.Clear();

            System.Console.WriteLine($"Step 2: Quantity Mocks - {ListMocks.Count}");

            DeserializeRestrictedUsers();

            System.Console.WriteLine($"Step 3: Quantity Mocks - {ListMocks.Count}");

            System.Console.WriteLine("Waiting ...");
            System.Console.ReadKey();
        }

        #region Serialize and Deserialize Methods
        private static void SerializeRestrictedUsers()
        {
            // If the file name does not provide
            if (string.IsNullOrWhiteSpace(FileName))
                return;

            // If the directory does not exist
            string directory = Path.GetDirectoryName(FileName);
            if ((!string.IsNullOrWhiteSpace(directory)) &&
                (!Directory.Exists(directory)))
                Directory.CreateDirectory(FileName);

            using (var writer = new FileStream(FileName, FileMode.Create, FileAccess.Write))
            {
                var serializer = new BinaryFormatter();
                serializer.Serialize(writer, ListMocks);
            }
        }

        private static void DeserializeRestrictedUsers()
        {
            // If the file name does not provide
            if (string.IsNullOrWhiteSpace(FileName))
                return;

            // If the file does not exist
            if (!File.Exists(FileName))
                return;

            using (var reader = new FileStream(FileName, FileMode.Open, FileAccess.Read))
            {
                var deserialize = new BinaryFormatter();
                ListMocks = (List<Mock>)deserialize.Deserialize(reader);
            }
        }
        #endregion
    }

    [Serializable]
    public class Mock
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
