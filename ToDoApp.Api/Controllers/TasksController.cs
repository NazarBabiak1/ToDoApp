﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Interfaces;
using ToDoApp.Data.Enums;
using System.Collections.Generic;

namespace ToDoApp.Api.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Data.Models.Task>>> GetAsync()
        {
            var items = await _service.GetAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateTaskDto taskDto)
        {
            await _service.CreateAsync(taskDto);
            return Ok();
        }

        [HttpPut("update-description-or-name/{taskId}")]
        public async Task<IActionResult> UpdateDescriptionOrNameAsync(int taskId, [FromBody] UpdateDescriptionOrNameDto updateDto)
        {
            await _service.UpdateDescriptionOrNameAsync(taskId, updateDto);
            return Ok();
        }

        [HttpPut("update-status/{taskId}")]
        public async Task<IActionResult> UpdateStatusAsync(int taskId, [FromBody] UpdateStatusDto updateStatusDto)
        {
            try
            {
                await _service.UpdateStatusAsync(taskId, updateStatusDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update-assignee/{taskId}")]
        public async Task<IActionResult> UpdateAssigneeAsync(int taskId, [FromBody] UpdateAssigneeDto updateAssigneeDto)
        {
            await _service.UpdateAssigneeAsync(taskId, updateAssigneeDto);
            return Ok();
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteAsync(int taskId)
        {
            await _service.DeleteAsync(taskId);
            return Ok();
        }
    }
}
