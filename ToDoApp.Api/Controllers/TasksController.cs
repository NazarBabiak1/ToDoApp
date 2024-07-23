using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;

namespace ToDoApp.Api.Controllers
{
    [Route("api/to-do-tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        private readonly ToDoContext _context;

        public TasksController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ToDoApp.Data.Models.Task>> GetAsync()
        {
            var items = await _context.Tasks.ToListAsync();
            return Ok(items);
        }
    }
}
