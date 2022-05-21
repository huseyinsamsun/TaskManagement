using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.AutoFac
{
   public class AutofacBusinessModel:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TaskProjectManager>().As<ITaskProjectService>();
            builder.RegisterType<EfTaskProjectDal>().As<ITaskProjectDal>();
            builder.RegisterType<UserTaskManager>().As<IUserTaskService>();
            builder.RegisterType<EfUserTaskDal>().As<IUserTaskDal>();
        }
    }
}
