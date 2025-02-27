﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class HistoryObor
    {
        [Key]
        public int Id { get; set; }
        public int IdUserr { get; set; }
        public int IdObor {  get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
    }
}
