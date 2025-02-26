using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Programs
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string VersionPO { get; set; }
        public int DeveloperId { get; set; }
        public int OborId { get; set; }
    }
}
