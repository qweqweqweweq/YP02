using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Settings
    {
        [Key]
        public int Id { get; set; }
        public string Ip { get; set; }
        public string Mask { get; set; }
        public string Gateway { get; set; }
        public string Dns { get; set; }
    }
}
