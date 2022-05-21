using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
 public class AssignTaskForUserDto
    {
        public int TaskId { get; set; }

        [Required]
        public PriotrityEnums Priotrity { get; set; }
        [Required]
        [StringLength(100)]
        public string EndDescription { get; set; }
    }
}
