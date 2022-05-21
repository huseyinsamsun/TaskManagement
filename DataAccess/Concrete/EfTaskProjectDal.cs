using DataAccess.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityFramework;
using Entities.Concrete;
using Entities.Abstract;
using DataAccess.Abstract;

namespace DataAccess.Concrete
{
 public class EfTaskProjectDal :EfEntityRepositoryBase<TaskProject, MyContext>, ITaskProjectDal
    {
    }
}
