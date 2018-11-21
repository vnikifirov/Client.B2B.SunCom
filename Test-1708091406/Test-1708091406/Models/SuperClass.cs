using System.ComponentModel.DataAnnotations;

namespace Test_1708091406.Models
{
    public class SuperParent
    {
        [Key]
        public int SuperClass_ID { get; set; }

        public string SuperParentText { get; set; }
    }

    public class Child_1 : SuperParent
    {
        [Key]
        public int Child_1_ID { get; set; }

        public string Child_1Tex { get; set; }
    }

    public class Parent : SuperParent
    {
        [Key]
        public int Parent_ID { get; set; }

        public string ParentText { get; set; }
    }

    public class Child_2 : Parent
    {
        [Key]
        public int Child_2_ID { get; set; }

        public string Child_2Text { get; set; }
    }
}
