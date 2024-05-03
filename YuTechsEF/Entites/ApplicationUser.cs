﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuTechsEF.Entites
{
    public class ApplicationUser:IdentityUser
    {
        public string? FirstName { get; set; }
    }
}
