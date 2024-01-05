# To-Do-App
<ul>
    <li><a href="#Introduction">Introduction</a></li>
    <li><a href="#CRUD Operations">CRUD Operations</a></li>
    <li><a href="#Additional Features">Additional Features</a></li>
    <li><a href="#Key Takeaways">Key Takeaways</a></li>
</ul>

<h1 id="Introduction">Introduction</h1>
<p>This README provides a summary of a To-Do app I created to demonstrate my knowledge of ASP.NET Core 8.0 and the MVC framework. Merging aesthetics with functionality, my goal was to deliver a user-friendly and visually pleasing app which also incorporated the features of ASP.NET Core. To provide the user with a feel of hand-written notes, which I felt complemented the theme of to-do lists, I utilized the "Sketchy" theme from Bootswatch.</p>

<h1 id="CRUD Operations">CRUD Operations</h1>
<p>The CRUD operations were pretty straighforward. Since the app is supposed to be a simple to-do list, I wanted to make sure users were able to quickly add tasks without having to go through too many entry fields. The task model includes three required properties: task name, category, and priority level. In order to streamline task entry, I created enumeration lists for category and priority level, each initialized with a default option. This way, the user is only required to enter the minimum of a task name to enter a task and move on. The user can also enter a date/time and description if applicable, or they can update these fields in the future. </p>

<h4>Task Model:</h4>

        public class TaskModel
        {
            [Key]
            public int Id { get; set; }
        
            [Required]
            [StringLength(50)]
            [DisplayName("Task Name")]
            public string taskName { get; set; }
        
            [StringLength(255)]
            [DisplayName("Description")]
            public string? description { get; set; }
        
            [Required]
            [EnumDataType(typeof(PriorityLevel))]
            [DisplayName("Priority")]
            public PriorityLevel priorityLevel { get; set; }
        
            [DisplayName("Time")]
            public DateTime? dueDate { get; set; }
        
            [Required]
            [EnumDataType(typeof(Category))]
            [DisplayName("Category")]
            public Category category { get; set; }
        
            [DisplayName("Completed")]
            public bool isComplete { get; set; }
        
        }

        public enum PriorityLevel
        {
            Low,
            Medium,
            High
        }
        
        public enum Category
        {
            Other,
            Work,
            Home,
            School,
            Appointment
        }

<h4>ToDo Controller (CRUD operations):</h4>

        private readonly ApplicationDbContext _db;
        public ToDoController(ApplicationDbContext db)
        {
            _db = db;
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
                TempData["success"] = "Task created successfully";
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
                TempData["success"] = "Task updated successfully";
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
        
<h1 id="Additional Features">Additional Features</h1>
<ul>
  <li><h4>Sorting Options</h4></li>
</ul>
    <p>To provide users with greater control over their to-do list, I implemented the option to sort tasks based on the properties of the task model. Users can easily arrange tasks in ascending or descending order by selecting the filter option and then choosing the desired property.</p>

    ![ToDo Sorting Gif](https://github.com/Anthony15651/To-Do-App/blob/main/GIFs/ToDoSorting.gif)
<ul>
  <li><h4>Toastr Notifications</h4></li>
</ul>

<h1 id="Key Takeaways">Key Takeaways</h1>
