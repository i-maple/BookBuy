﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDApp.Models
{
    public class CoverType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
    }
}
