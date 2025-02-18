using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class MainCharacMaterials
    {
        [Key]
        public int Id { get; set; }
        public string Color { get; set; }
        public double Cost { get; set; }
        public string Size { get; set; }
    }
}
