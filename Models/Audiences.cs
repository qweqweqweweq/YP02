using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Audiences
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviated { get; set; }
        public int IdUser { get; set; }
        public string TempUser { get; set; }
    }
}
