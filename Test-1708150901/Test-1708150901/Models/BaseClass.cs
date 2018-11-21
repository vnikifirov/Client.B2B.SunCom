using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_1708150901.Models
{
    [Table("BaseClass")]
    public class BaseClass
    {
        [Key]
        public int ID { get; set; }

        [Column("GUID")]
        public string GUID { get; set; }
    }
}
