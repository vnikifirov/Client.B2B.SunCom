using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_1708291323
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> nums1 = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            List<int> nums2 = new List<int> { 1, 2, 3 };

            List<int> res = nums1.Except(nums2).ToList();

            foreach (var item in res)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}
