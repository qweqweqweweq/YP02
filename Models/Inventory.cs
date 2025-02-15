using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string Name { get; set; }
    }
}
