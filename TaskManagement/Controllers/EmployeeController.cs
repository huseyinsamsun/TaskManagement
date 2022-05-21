using Business.Abstract;
using Core.Helper;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TaskManagement.EnumExtensionsManager;

namespace TaskManagement.Controllers
{
    public class EmployeeController : Controller
    {
        MyContext context = new MyContext();
        public UserManager<AppUser> _userManager;
        public RoleManager<AppRole> _roleManager;
        public SignInManager<AppUser> _signInManager;
        private readonly ITaskProjectService _task;
        private IUserTaskService _userTaskService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmployeeController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, ITaskProjectService task, IUserTaskService userTaskService,IWebHostEnvironment  webHostEnviroment)
        {
            _webHostEnvironment= webHostEnviroment;
            _userTaskService = userTaskService;
            _task = task;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetTask()
        {
 
      
            List<UserTaskForEmployeeDto> employeeDtos = new List<UserTaskForEmployeeDto>();
            AppUser appUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            TempData["userId"]=appUser.Id;
            var result = (from ut in _userTaskService.GetList().ToList()
                          join t in _task.GetList().ToList() on ut.TaskId equals t.Id
                          select new
                          {
                              ut.UserId,
                              ut.TaskId,
                              t.Title,
                              t.Id,
                              ut.Status,
                              ut.Priotrity,
                              ut.EndDescription,
                              t.CompletionTime



                          }).Where(x=>x.UserId==appUser.Id&&x.TaskId==x.Id);



            foreach (var item in result)
            {
           
                employeeDtos.Add(new UserTaskForEmployeeDto()
                {
                    CompletionTime= item.CompletionTime,
                    Taskİd=item.TaskId,
                    Title= item.Title,
                    Status=item.Status,
                     Priotrity = item.Priotrity
                });
               
            }


            return View(employeeDtos);
        }
       public IActionResult ChangeStatus(int enumValue,int taskİd)
        {
            AppUser appUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            UserTaskDto userTask = new UserTaskDto();
           var result=  _userTaskService.GetByBothId(appUser.Id,taskİd);
            userTask.Id = result.Id;
            userTask.UserId=appUser.Id;
            userTask.TaskId = taskİd;
            userTask.Priotrity = result.Priotrity;
            TempData["taskid"] = taskİd;
      
   
            

            if (enumValue==0)
            {
                if (result.Status!= StatusEnums.Wait)
                {
               
                  

                    userTask.Status = StatusEnums.Wait;
                    _userTaskService.Update(userTask);
                  
                }
                return RedirectToAction("GetTask", "Employee");
            }
            else if (enumValue==1)
            {
                if (result.Status!=StatusEnums.Continuing)
                {
             
                    userTask.Status = StatusEnums.Continuing;
                    _userTaskService.Update(userTask);
                
                 

                }
                return RedirectToAction("GetTask", "Employee");
            }
            else
            {
                if (result.Status!=StatusEnums.Completed)
                {
                    CompletedTaskHelper.CompletedTaskEmail()


                    userTask.Status = StatusEnums.Completed;
                    _userTaskService.Update(userTask);
                    return RedirectToAction("CompletedTask", "Employee");
                }
                return RedirectToAction("GetTask", "Employee");
            }

      
        }
        public IActionResult CompletedTask()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CompletedTask(CompletedTaskDto completedTaskDto)
        {
            AppUser appUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            UserTaskDto userTaskDto = new UserTaskDto();
          
            if (completedTaskDto.DocumentName!=null)
            {
                var extension = Path.GetExtension(completedTaskDto.DocumentName.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/dosyalar/",newimagename);
                var stream = new FileStream(location, FileMode.Create);
                completedTaskDto.DocumentName.CopyTo(stream);
                userTaskDto.DocumentName = newimagename;

            }
            var result = _userTaskService.GetByBothId(appUser.Id, (int)TempData["taskid"]);
            userTaskDto.UserId = appUser.Id;
            userTaskDto.Status=result.Status;
            userTaskDto.Priotrity = result.Priotrity;
            userTaskDto.EndDescription = completedTaskDto.EndDescription;
            userTaskDto.Id = result.Id;

            userTaskDto.TaskId = (int)TempData["taskid"];
            _userTaskService.Update(userTaskDto);





            return View(completedTaskDto);
        }
     

    }
}
