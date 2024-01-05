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
><h3>Sorting Options</h3>
<p>To provide users with greater control over their to-do list, I implemented the option to sort tasks based on the properties of the task model. Users can easily arrange tasks in ascending or descending order by selecting the filter option and then choosing the desired property.</p>

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

![ToDo Sorting Gif](https://github.com/Anthony15651/To-Do-App/blob/main/GIFs/ToDoSorting.gif)

<h3>Task Completion</h3>
<p>Rather than having users delete tasks after completing them, I created a way to check them off the list. By doing so, the task is moved to a "Completed" section, allowing users to review completed tasks or uncheck them if needed. At first, I was reloading the page each time a task was checked to update the lists, but this approach felt choppy and would cause the page to jump around. To overcome this, I used JQuery and AJAX to update the DOM each time a task is marked complete/uncomplete, resulting in a smooth transition as tasks move between lists. </p>

        <!-- Table (ToDo Task List) -->
        <table class="table table-striped border border-primary" id="toDoTasks">
            <tbody>
                @foreach (var obj in Model.Where(task => !task.isComplete))
                {
                    <tr>
                        <td class="col-1 text-center align-middle">
                            <a asp-action="ToggleCompletion" data-task-id="@obj.Id"><i class="bi bi-circle fs-1"></i></a>
                        </td>
                        <td class="col-3">
                            <div class="container">
                                <div class="col">
                                    <div class="row fs-3">
                                        @obj.taskName
                                    </div>
                                    <div class="row">
                                        @* Time formatted to remove "seconds" *@
                                        @obj.dueDate?.ToString("MM/dd/yyyy hh:mm tt")
                                    </div>
                                    <div class="row">
                                        @obj.category
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td class="align-middle text-center col-4">
                            @obj.description
                        </td>
                        <td class="align-middle text-center col-2">
                            @obj.priorityLevel <br />@("priority")
                        </td>
                        <td class="align-middle col-1">
                            <div class="container text-end">
                                <div class="fs-2">
                                    <a asp-controller="ToDo" asp-action="Edit" asp-route-id="@obj.Id"><i class="bi bi-pencil-square"></i></a>
                                </div>
                                <div class="fs-2">
                                    <a asp-action="" data-bs-toggle="modal" data-bs-target="#deleteModal" data-task-id="@obj.Id"><i class="bi bi-trash-fill"></i></a>
                                </div>
                            </div>
                        </td>
                    </tr>
                }
            </tbody>
        </table>

        // Script to handle check button
        $(document).ready(function () {
            $('a[data-task-id]').on('click', function (e) {
                e.preventDefault();
                // "if" to make sure clicking delete icon does not toggle completion status
                if ($(this).find('i').hasClass('bi-trash-fill') == false) {
                    var taskId = $(this).data('task-id');
                    var clickedRow = $(this).closest('tr');
                    $.post('/ToDo/ToggleCompletion', { taskId: taskId }, function (data) {
                        if (data.success) {
                            if (data.isComplete) {
                                // Move to Completed table and updat icon to be checked
                                clickedRow.find('i').removeClass('bi-circle').addClass('bi-check-circle-fill');
                                $('#completedTasks tbody').append(clickedRow);
                            } else {
                                // Move to ToDo table and update icon to be open circle
                                clickedRow.find('i').removeClass('bi-check-circle-fill').addClass('bi-circle');
                                $('#toDoTasks tbody').append(clickedRow);
                            }
                        } else {
                            console.log('Error toggling completion status.');
                        }
                    });
                }
            });
        });


<ul>
  <li><h3>Toastr Notifications</h3></li>
</ul>

<h1 id="Key Takeaways">Key Takeaways</h1>
