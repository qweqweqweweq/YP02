using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string NumberEuip { get; set; }
        public int IdAudience { get; set; }
        public int IdUser { get; set; }
        public string TempUser { get; set; }
        public double PriceEquip { get; set; }
        public string DirectionEquip { get; set; }
        public string Status { get; set; }
        public string Model { get; set; }
        public string Comment { get; set; }
    }
}
