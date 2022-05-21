using Business.Abstract;
using DataAccess.Abstract;
using Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TaskProjectManager : ITaskProjectService
    {
        private ITaskProjectDal _taskProjectDal;
        public TaskProjectManager(ITaskProjectDal taskProjectDal)
        {
            _taskProjectDal = taskProjectDal;
        }
        public void Add(TaskProject taskProject)
        {
            _taskProjectDal.Add(taskProject);
        }

        public void Delete(TaskProject taskProject)
        {
            _taskProjectDal.Delete(taskProject);
        }

        public TaskProject GetById(int taskId)
        {
            return _taskProjectDal.Get(filter: x => x.Id == taskId).Result;
        }

        public List<TaskProject> GetList()
        {
            return _taskProjectDal.GetList().Result.ToList();
        }

        public void Update(TaskProject taskProject)
        {
            _taskProjectDal.Update(taskProject);
        }
    }
}
