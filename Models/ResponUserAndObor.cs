using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class ResponUserAndObor
    {
        [Key]
        public int Id { get; set; }
        public int UsId { get; set; }
        public int OborudId { get; set; }
        public string Comment { get; set; }
    }
}
