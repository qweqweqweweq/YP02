using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Materials
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Image { get; set; }
        public string Count { get; set; }
        public int IdUser { get; set; }
        public string TempUser { get; set; }
        public string Type { get; set; }
    }
}
