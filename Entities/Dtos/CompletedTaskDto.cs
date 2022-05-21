using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
public class CompletedTaskDto
    {
        public int TaskId { get; set; }
        public IFormFile DocumentName { get; set; }
        public string EndDescription { get; set; }


    }
}
