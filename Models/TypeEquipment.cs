﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class TypeEquipment
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
