using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            List<TaskModel> objTaskList = _db.Tasks.ToList();
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
    }
}
