using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Enums;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using Task = System.Threading.Tasks.Task;

namespace ToDoApp.Services.Interfaces
{
    public interface ITaskService
    {
        Task<List<ToDoApp.Data.Models.Task>> GetAsync();
        Task CreateAsync(CreateTaskDto taskDto);
        Task UpdateDescriptionOrNameAsync(int taskId, string title, string description);
        Task UpdateStatusAsync(int taskId, Status status);
        Task UpdateAssigneeAsync(int taskId, int assigneeId);
        Task DeleteAsync(int taskId);
    }
}
