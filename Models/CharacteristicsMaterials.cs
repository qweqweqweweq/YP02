using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class CharacteristicsMaterials
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaterialsRasxod { get; set; }
        public string ArbitratyValue { get; set; }
        public int RasxodType { get; set; }
        public int IdMainCharacMaterials { get; set; }
    }
}
