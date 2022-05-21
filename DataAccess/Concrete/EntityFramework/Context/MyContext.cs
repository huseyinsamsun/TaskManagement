using Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Context
{
public class MyContext:IdentityDbContext<AppUser,AppRole,string>
    {
        public MyContext()
        {

        }
        public MyContext(DbContextOptions<MyContext>options):base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-OB25F2G\\SQLEXPRESS;Database=TaskProjectManagementDB;Trusted_Connection=True;");
        }

  
        public virtual DbSet<TaskProject> TaskProjects { get; set; }
        public virtual DbSet<UserTaskDto> UserTaskDtos { get; set; }


    }
}
