﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Models.Compensations
{
    public class Adjustment : Compensation
    {
        public string Name { get; set; }
    }
}
