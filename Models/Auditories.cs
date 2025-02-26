using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Auditories
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int ResponUser { get; set; }
        public int TimeResponUser { get; set; }
    }
}
