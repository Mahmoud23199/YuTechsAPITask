﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuTechsEF.CustomValidation;

namespace YuTechsBL.Dtos
{
    public class AuthRelatNewsDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public string NewsContent { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [ValidPublicationDate]
        public DateTime PublicationDate { get; set; }

        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
