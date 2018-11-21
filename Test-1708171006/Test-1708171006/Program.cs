using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Test_1708171006
{
    class Program
    {
        static void Main(string[] args)
        {
            //var g = new Person("Bob Bobovich", 21, Gender.Male);
            //string json = string.Empty;

            //Console.OutputEncoding = Encoding.UTF8;
            //Console.WriteLine("Serialized object:");
            //Console.WriteLine(json = JsonConvert.SerializeObject(g, new StringEnumConverter()));

            //try
            //{
            //    Person person = JsonConvert.DeserializeObject<Person>(json);

            //    Console.WriteLine("Deserialized object:");
            //    Console.WriteLine(person.ToString());
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.ToString());
            //}

            //Gender gender = Gender.Female;
            //string json = string.Empty;
            //string not_expexted_str = "Vasy";

            //Console.OutputEncoding = Encoding.UTF8;
            //Console.WriteLine("Serialized object:");
            //Console.WriteLine(json = JsonConvert.SerializeObject(gender, new StringEnumConverter()));

            //try
            //{
            //    Gender des_gender = JsonConvert.DeserializeObject<Gender>(not_expexted_str, new StringEnumConverter());

            //    Console.WriteLine("Deserialized object:");
            //    Console.WriteLine(des_gender.ToString());
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.ToString());
            //}

            //var dictionary =
            //    Enum.GetValues(typeof(Gender)).Cast<object>()
            //        .ToDictionary(e => (int)e, e => JsonConvert.SerializeObject(e, new StringEnumConverter()));

            //var jsonData = JsonConvert.SerializeObject(dictionary);

            var jsonData =
                Enum.GetValues(typeof(Gender)).Cast<object>()
                    .Select(e => new
                    {
                        id = (int)e,
                        name = JsonConvert.SerializeObject(e, new StringEnumConverter())
                    }).ToArray();
           
            Console.OutputEncoding = Encoding.UTF8;
            foreach (var item in jsonData)
            {
                Console.WriteLine(item.name);
            }

            Console.ReadKey();
        }
    }      

    public enum Gender
    {
        [EnumMember(Value = "Пусто")]
        None,
        [EnumMember(Value = "Мужчина")]
        Male,
        [EnumMember(Value = "Женщина")]
        Female
    }

    public class Person
    {

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public Person(string name, int age, Gender gender)
        {
            this.Name = name;
            this.Age = age;
            this.Gender = gender;
        }

        public override string ToString()
        {
            return string.Format("Person [{0}, {1}, {2}]", Name, Age, Gender);
        }
    }  
}
