using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Data.Context;
using ToDoApp.Data.Enums;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Exceptions;
using ToDoApp.Services.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace ToDoApp.Services.Services
{
    public class TaskService : ITaskService
    {
        private readonly ToDoContext _context;
        private readonly ICurrentUserService _currentUserService;

        public TaskService(ToDoContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<List<Data.Models.Task>> GetAsync()
        {
            return await _context.Tasks
                .Where(x => x.CreatedById == _currentUserService.UserId)
                .ToListAsync();
        }

        public async Task CreateAsync(CreateTaskDto taskDto)
        {
            var item = new Data.Models.Task
            {
                AssigneeId = taskDto.AssigneeId,
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                BoardId = taskDto.BoardId,
                Status = new Status
                {
                    Name = ActivityStatus.ToDo.ToString()
                },
                CreatedById = _currentUserService.UserId,
            };
            _context.Tasks.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDescriptionOrNameAsync(int taskId, string title, string description)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                if (!string.IsNullOrEmpty(title))
                {
                    task.Title = title;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    task.Description = description;
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateStatusAsync(int taskId, ActivityStatus newStatus)
        {
            var task = await _context.Tasks
                .Include(t => t.Status)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
            {
                throw new TaskNotFoundException(taskId);
            }

            var currentStatus = Enum.Parse<ActivityStatus>(task.Status.Name);

            if (!IsValidStatusTransition(currentStatus, newStatus))
            {
                throw new InvalidStatusTransitionException(currentStatus, newStatus);
            }

            task.Status.Name = newStatus.ToString();
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAssigneeAsync(int taskId, int assigneeId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                task.AssigneeId = assigneeId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        private bool IsValidStatusTransition(ActivityStatus currentStatus, ActivityStatus newStatus)
        {
            switch (currentStatus)
            {
                case ActivityStatus.ToDo:
                    return newStatus == ActivityStatus.InProgress;
                case ActivityStatus.InProgress:
                    return newStatus == ActivityStatus.ToDo || newStatus == ActivityStatus.Done;
                case ActivityStatus.Done:
                    return false;
                default:
                    return false;
            }
        }
    }
}
