using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_1708150901.Data;
using Test_1708150901.Models;

namespace Test_1708150901
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new MyDbContext();

            //foreach (var item in context.Models)
            //{
            //    if (item is Child1 c1)
            //    {
            //        Console.WriteLine($"Child 1 - ID {c1.ID}, Created - {c1.Created}");
            //    }

            //    if (item is Child2 c2)
            //    {
            //        Console.WriteLine($"Child 2 - ID {c2.ID}, GUID - {c2.Message}");
            //    }
            //}

            BaseClass instance = context.Models.FirstOrDefault();
            context.Models.Remove(instance);

            // Create models 
            //var model_1 = new Child1 { GUID = Guid.NewGuid().ToString(), Created = DateTime.Now };
            //var model_2 = new Child2 { Message = "Test message form the 'model_2'" };

            //context.Models.Add(model_1);
            //context.Models.Add(model_2);

            context.SaveChanges();

            Console.ReadKey();
        }        
    }
}
