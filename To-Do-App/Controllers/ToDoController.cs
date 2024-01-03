using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using To_Do_App.Data;
using To_Do_App.Models;

namespace To_Do_App.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ToDoController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string sortOrder)
        {
            ViewData["IdSortParam"] = string.IsNullOrEmpty(sortOrder) || sortOrder != "id_desc" ? "id_desc" : "id";
            ViewData["CategorySortParam"] = sortOrder == "category" ? "category_desc" : "category";
            ViewData["TaskNameSortParam"] = sortOrder == "taskName" ? "taskName_desc" : "taskName";
            ViewData["PrioritySortParam"] = sortOrder == "priority" ? "priority_desc" : "priority";
            ViewData["DueDateSortParam"] = sortOrder == "dueDate" ? "dueDate_desc" : "dueDate";

            IQueryable<TaskModel> tasks = _db.Tasks.AsQueryable();

            switch (sortOrder)
            {
                case "id":
                    tasks = tasks.OrderBy(task => task.Id);
                    break;
                case "id_desc":
                    tasks = tasks.OrderByDescending(task => task.Id);
                    break;
                case "category":
                    tasks = tasks.OrderBy(task => task.category);
                    break;
                case "category_desc":
                    tasks = tasks.OrderByDescending(task => task.category);
                    break;
                case "taskName":
                    tasks = tasks.OrderBy(task => task.taskName);
                    break;
                case "taskName_desc":
                    tasks = tasks.OrderByDescending(task => task.taskName);
                    break;
                case "priority":
                    tasks = tasks.OrderBy(task => task.priorityLevel);
                    break;
                case "priority_desc":
                    tasks = tasks.OrderByDescending(task => task.priorityLevel);
                    break;
                case "dueDate":
                    tasks = tasks.OrderBy(task => task.dueDate);
                    break;
                case "dueDate_desc":
                    tasks = tasks.OrderByDescending(task => task.dueDate);
                    break;
                default:
                    tasks = tasks.OrderByDescending(task => task.Id);
                    break;
            }

            List<TaskModel> objTaskList = tasks.ToList();
            return View(objTaskList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TaskModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.Tasks.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "ToDo");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            TaskModel? taskModelFromDb = _db.Tasks.Find(id);
            if(taskModelFromDb == null)
            {
                return NotFound();
            }
            return View(taskModelFromDb);
        }

        [HttpPost]
        public IActionResult Edit(TaskModel taskModelObj)
        {
            if(ModelState.IsValid)
            {
                _db.Tasks.Update(taskModelObj);
                _db.SaveChanges();
                return RedirectToAction("Index", "ToDo");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            TaskModel? taskModelFromDb = _db.Tasks.Find(id);
            if (taskModelFromDb == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "ToDo");
        }

        [HttpPost, ActionName("DeleteTask")]
        public IActionResult DeletePOST(int? taskId)
        {
            TaskModel? taskModelFromDb = _db.Tasks.Find(taskId);
            if (taskModelFromDb == null)
            {
                return NotFound();
            }
            _db.Tasks.Remove(taskModelFromDb);
            _db.SaveChanges();
            return RedirectToAction("Index", "ToDo");
        }

        [HttpPost]
        public IActionResult ToggleCompletion(int taskId)
        {
            TaskModel taskModel = _db.Tasks.Find(taskId);
            if (taskModel == null)
            {
                return Json(new { Success = false });
            }
            taskModel.isComplete = !taskModel.isComplete;
            _db.SaveChanges();

            //return RedirectToAction("Index", "ToDo");
            return Json(new { success = true, isComplete = taskModel.isComplete });
        }
    }
}
