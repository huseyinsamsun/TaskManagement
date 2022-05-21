using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserTaskForEmployeeDto
    {
        public int Taskİd { get; set; }
        public string Title { get; set; }
        public StatusEnums Status { get; set; }
        public PriotrityEnums Priotrity { get; set; }
        public DateTime CompletionTime { get; set; }


    }
}
