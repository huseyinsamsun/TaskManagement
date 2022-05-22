using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class UserTaskDto : BaseEntity
    {
        public string UserId { get; set; }
        public int TaskId { get; set; }
        public StatusEnums Status { get; set; }
        public PriotrityEnums Priotrity { get; set; }
        public string DocumentName { get; set; }
        public string EndDescription { get; set; }
        public string ManagerId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual TaskProject TaskProject { get; set; }
  



    }
}
