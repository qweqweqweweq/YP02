﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}
