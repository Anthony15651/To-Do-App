using Microsoft.AspNetCore.Mvc;
using To_Do_App.Data;
using To_Do_App.Models;

namespace To_Do_App.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TaskController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<TaskModel> objTaskList = _db.Tasks.ToList();
            return View(objTaskList);
        }
    }
}
