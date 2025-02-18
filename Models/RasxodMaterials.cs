using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class RasxodMaterials
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DatePostupleniya { get; set; }
        public Blob Photo { get; set; }
        public double Quantity { get; set; }
        public int UserRespon { get; set; }
        public int ResponUserTime { get; set; }
        public int TypeRasxod { get; set; }
    }
}
