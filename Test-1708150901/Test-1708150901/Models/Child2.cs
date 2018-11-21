using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_1708150901.Models
{
    [Table("Child2")]
    public class Child2 : BaseClass
    {
        [Column("Message")]
        public string Message { get; set; }
    }
}
