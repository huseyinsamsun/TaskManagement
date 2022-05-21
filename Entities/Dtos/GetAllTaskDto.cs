using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
   public class GetAllTaskDto
    {
        public string FullName { get; set; }
        public string Title { get; set; }
        public string ContentTask { get; set; }
        public StatusEnums Status { get; set; }
        public PriotrityEnums Priotrity { get; set; }
        public DateTime CompleteTime { get; set; }
        public string DocumentName { get; set; }
        public string EndDescription { get; set; }

    }
}
