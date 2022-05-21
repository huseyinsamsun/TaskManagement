using Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
public class TaskProject:BaseEntity
    {
        public TaskProject()
        {
            UserTaskDtos = new HashSet<UserTaskDto>();
        }
        public string Title { get; set; }
        public string ContentTask { get; set; }
        public DateTime CompletionTime { get; set; }

        public string DocumentName { get; set; }


        public virtual ICollection<UserTaskDto> UserTaskDtos { get; set; }





    }
}
