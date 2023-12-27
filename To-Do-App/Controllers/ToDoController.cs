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
    }
}
