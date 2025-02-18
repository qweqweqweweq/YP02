using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class HistoryInventory
    {
        [Key]
        public int Id { get; set; }
        public int OborId { get; set; }
        public int IdUser { get; set; }
    }
}
