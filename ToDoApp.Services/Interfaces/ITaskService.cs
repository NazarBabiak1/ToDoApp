using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Data.Enums;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using Task = System.Threading.Tasks.Task;

namespace ToDoApp.Services.Interfaces
{
    public interface ITaskService
    {
        Task<List<Data.Models.Task>> GetAsync();
        Task CreateAsync(CreateTaskDto taskDto);
        Task UpdateDescriptionOrNameAsync(int taskId, string title, string description);
        Task UpdateStatusAsync(int taskId, ActivityStatus newStatus);
        Task UpdateAssigneeAsync(int taskId, int assigneeId);
        Task DeleteAsync(int taskId);
    }
}
