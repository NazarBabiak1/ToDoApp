using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Interfaces;
using ToDoApp.Services.Services;

namespace ToDoApp.Api.Controllers
{
    [Route("api/boards")]
    [ApiController]
    public class BoardsController : ControllerBase
    {

        private readonly IBoardService _service;

        public BoardsController(IBoardService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Board>> GetAsync()
        {
            var items = await _service.GetAsync();
            return Ok(items);
        }
    }
}
