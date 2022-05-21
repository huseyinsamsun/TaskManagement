using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserTaskManager : IUserTaskService
    {
        private IUserTaskDal _userTaskDal;
        public UserTaskManager(IUserTaskDal userTaskDal)
        {
            _userTaskDal = userTaskDal;
        }

        public void Add(UserTaskDto userTaskDto)
        {
            _userTaskDal.Add(userTaskDto);
           
        }

        public void Delete(UserTaskDto userTaskDto)
        {
            _userTaskDal.Delete(userTaskDto);
        }

        public UserTaskDto GetByBothId(string userId, int taskId)
        {
          return _userTaskDal.Get(filter: x => x.UserId == userId && x.TaskId == taskId).Result;
        }

        public UserTaskDto GetByTaskId(int taskId)
        {
             return _userTaskDal.Get(filter: x => x.TaskId == taskId).Result;
        }

        public UserTaskDto GetByUserId(string userId)
        {
           return  _userTaskDal.Get(filter: x => x.UserId == userId).Result;
        }

        public List<UserTaskDto> GetList()
        {
           return  _userTaskDal.GetList().Result.ToList();
        }

        public void Update(UserTaskDto userTaskDto)
        {
            _userTaskDal.Update(userTaskDto);
        }
    }
}
