using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_1708091406.Data;

namespace Test_1708091406
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ModelContext())
            {
                context.SuperParents.Add(new Models.SuperParent
                {
                    SuperParentText = "SuperParent"
                });

                context.SuperParents.Add(new Models.Parent
                {
                    ParentText = "Parent"
                });

                context.SuperParents.Add(new Models.Child_1
                {
                    Child_1Tex = "Child 1"
                });

                context.SuperParents.Add(new Models.Child_1
                {
                    Child_1Tex = "Child 1.2"
                });

                context.SuperParents.Add(new Models.Child_1
                {
                    Child_1Tex = "Child 1.3"
                });

                context.SuperParents.Add(new Models.Child_2
                {
                    Child_2Text = "Child 2"
                });

                context.SuperParents.Add(new Models.Child_2
                {
                    Child_2Text = "Child 2.1"
                });

                context.SuperParents.Add(new Models.Child_2
                {
                    Child_2Text = "Child 2.2"
                });


                context.SaveChanges();

                Console.ReadKey();
            }
        }
    }
}
