using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserForLoginDto
    {
        [Required]
        public string EMail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
