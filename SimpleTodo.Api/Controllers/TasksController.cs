using Microsoft.AspNetCore.Mvc;

namespace SimpleTodo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private static readonly string[] Tasks =
    [
        "Complete project proposal",
        "Buy groceries",
        "Call mom",
        "Finish reading book",
        "Go for a run",
        "Write report",
        "Attend meeting",
        "Study for exam",
        "Pay bills",
        "Clean the house"
    ];

    private readonly ILogger<TasksController> _logger;

    public TasksController(ILogger<TasksController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetTasks")]
    public IEnumerable<string> Get()
    {
        return Tasks;
    }
}
