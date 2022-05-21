using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
  public interface ITaskProjectService
    {
        TaskProject GetById(int taskId);
        List<TaskProject> GetList();
        void Add(TaskProject  taskProject);
        void Update(TaskProject  taskProject);
        void Delete(TaskProject  taskProject);
    }
}
