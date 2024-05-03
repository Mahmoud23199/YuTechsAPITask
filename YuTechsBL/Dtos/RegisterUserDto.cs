using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuTechsBL.Dtos
{
    public class RegisterUserDto
    {
        //public string? FirstName { set; get; }
        [Required]
        public string UserName { set; get; }
        [Required]
        public string Password { set; get; }
        //[Required]
        //public string Email { set; get; }
       
    }
}
