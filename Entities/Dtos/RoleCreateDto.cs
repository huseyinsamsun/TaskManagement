using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
   public class RoleCreateDto
    {
        [Required]
        [StringLength(10)]
        public string Name { get; set; }
        public string Id { get; set; }
    }
}
