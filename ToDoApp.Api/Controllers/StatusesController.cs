using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Api.Controllers
{
    [Route("api/statuses")]
    [ApiController]
    public class StatusesController : ControllerBase
    {

        private readonly IStatusService _service;

        public StatusesController(IStatusService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Status>> GetAsync()
        {
            var items = await _service.GetAsync();
            return Ok(items);
        }
    }
}
