using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
   public interface IUserTaskService
    {
        UserTaskDto GetByUserId(string userId);
        UserTaskDto GetByTaskId(int taskId);
        UserTaskDto GetByBothId(string userId, int taskId);
        List<UserTaskDto> GetList();


        void Add(UserTaskDto  userTaskDto);
        void Update(UserTaskDto userTaskDto);
        void Delete(UserTaskDto  userTaskDto);
    }
}
