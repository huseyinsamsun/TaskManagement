using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.Controllers
{

    //[Authorize(Roles = "Yönetici")]
    public class ManagerController : Controller
    {
        public UserManager<AppUser> _userManager;
        public RoleManager<AppRole> _roleManager;
        public SignInManager<AppUser> _signInManager;
        private readonly ITaskProjectService _task;
        private IUserTaskService _userTaskService;
        public ManagerController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager,ITaskProjectService task,IUserTaskService userTaskService)
        {
            _userTaskService = userTaskService;
            _task = task;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public IActionResult GetAllTask()
        {
            List<GetAllTaskDto> getAllTaskDtos = new List<GetAllTaskDto>();
            var result = (from ut in _userTaskService.GetList().ToList()
                          join t in _task.GetList().ToList() on ut.TaskId equals t.Id
                          join u in _userManager.Users.ToList() on ut.UserId equals u.Id
                          select new
                          {
                              FullName = u.FirstName + " " + u.LastName,
                              t.Title,
                              t.ContentTask,
                              ut.Status,
                              ut.Priotrity,
                              t.CompletionTime,
                              ut.DocumentName,
                              ut.EndDescription


                          }).ToList();
            foreach (var item in result)
            {
                getAllTaskDtos.Add(new GetAllTaskDto()
                {
                    FullName = item.FullName,
                    Title = item.Title,
                    ContentTask = item.ContentTask,
                    Status = item.Status,
                    Priotrity = item.Priotrity,
                    CompleteTime = item.CompletionTime,
                    DocumentName = item.DocumentName,
                    EndDescription = item.EndDescription
                });
            }



            return View(getAllTaskDtos);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RoleCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RoleCreate(RoleCreateDto roleCreateDto)
        {
            AppRole role = new AppRole();
            role.Name = roleCreateDto.Name;
            IdentityResult result = _roleManager.CreateAsync(role).Result;
            if (result.Succeeded)
            {

                return RedirectToAction("Roles");
            }
            else
            {
                AddModelError(result);
            }

            return View(roleCreateDto);
        }
        public IActionResult EmployeeList()
        {
            return View(_userManager.Users.ToList());
        }
        public IActionResult Roles()
        {
            return View(_roleManager.Roles.ToList());
        }
        public void AddModelError(IdentityResult result)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
        }
        public IActionResult RoleAssign(string id)
        {
            TempData["userId"] = id;
            AppUser user = _userManager.FindByIdAsync(id).Result;
            ViewBag.userName = user.UserName;
            IQueryable<AppRole> roles = _roleManager.Roles;
            List<string> userRoles = _userManager.GetRolesAsync(user).Result as List<string>;
            List<RoleAssignDto> roleAssignDtos = new List<RoleAssignDto>();
            foreach (AppRole role in roles)
            {
                RoleAssignDto r = new RoleAssignDto();
                r.RoleId = role.Id;
                r.RoleName = role.Name;
                if (userRoles.Contains(role.Name))
                {
                    r.Exist = true;
                }
                else
                {
                    r.Exist = false;
                }
                roleAssignDtos.Add(r);

            }
            return View(roleAssignDtos);
        }


        [HttpPost]
        public async Task<IActionResult> RoleAssign(List<RoleAssignDto> roleAssignDtos)
        {
            AppUser user = await _userManager.FindByIdAsync(TempData["userId"].ToString());
            foreach (var item in roleAssignDtos)
            {
                if (item.Exist)
                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }
            return RedirectToAction("EmployeeList");
        }
        public IActionResult CreateTask()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateTask(CreateTaskDto createTaskDto)
        {
            TaskProject task = new TaskProject();
            task.Title = createTaskDto.Title;
            if (createTaskDto.CompletionTime<System.DateTime.Now)
            {
                task.CompletionTime = System.DateTime.Now.AddDays(5);
            }
            else
            {
                task.CompletionTime = createTaskDto.CompletionTime;
            }
          
            task.ContentTask = createTaskDto.ContentTask;
            task.DocumentName = createTaskDto.DocumentName;
          
            _task.Add(task);
                
            return View(createTaskDto);
        }
        public IActionResult AssignTask(string id)
        {
            TempData["id"] = id;
           var  taskList = _task.GetList().ToList();
            Dictionary<int, string> task = new Dictionary<int, string>();
            foreach (var item in  taskList)
            {
                task.Add(item.Id, item.Title);
            }
            ViewBag.Task = new SelectList(task, "Key", "Value");

            return View();
    
        }

        [HttpPost]
        public IActionResult AssignTask(AssignTaskForUserDto assignTaskForUserDto)
        {
            UserTaskDto userTaskDto = new UserTaskDto();
            userTaskDto.UserId = TempData["id"].ToString();
            userTaskDto.TaskId = assignTaskForUserDto.TaskId;
            userTaskDto.Priotrity = assignTaskForUserDto.Priotrity;
            _userTaskService.Add(userTaskDto);

            return View();
        }




    }
}
