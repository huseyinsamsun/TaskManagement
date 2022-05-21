using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
  public class CreateTaskDto
    {
        [Required]
        [StringLength(20)]
        public string Title { get; set; }
        [Required]
        [StringLength(80)]
        public string ContentTask { get; set; }
        public DateTime CompletionTime { get; set; }

        public string DocumentName { get; set; }
    
    }
}
