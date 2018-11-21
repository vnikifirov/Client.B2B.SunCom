using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_1708150901.Models
{
    [Table("Child1")]
    public class Child1 : BaseClass
    {
        [Column("Date_Created")]
        public DateTime Created { get; set; }
    }
}
