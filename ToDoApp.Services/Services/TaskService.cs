using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Data.Context;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Interfaces;
using ToDoApp.Data.Enums;
using ToDoApp.Data.Models;
using Task = System.Threading.Tasks.Task;

namespace ToDoApp.Services.Services;

public class TaskService : ITaskService
{
    private ToDoContext _context;
    private ICurrentUserService _currentUserService;

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
        if (task != null && task.CreatedById == _currentUserService.UserId)
        {
            task.Title = title;
            task.Description = description;
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateStatusAsync(int taskId, Status status)
    {
        var task = await _context.Tasks.FindAsync(taskId);
        if (task != null && task.CreatedById == _currentUserService.UserId)
        {
            // Add any additional status validation logic here
            task.Status.Name = status.Name;
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateAssigneeAsync(int taskId, int assigneeId)
    {
        var task = await _context.Tasks.FindAsync(taskId);
        if (task != null && task.CreatedById == _currentUserService.UserId)
        {
            task.AssigneeId = assigneeId;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int taskId)
    {
        var task = await _context.Tasks.FindAsync(taskId);
        if (task != null && task.CreatedById == _currentUserService.UserId)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
