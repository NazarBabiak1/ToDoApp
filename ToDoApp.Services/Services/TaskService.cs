using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Enums;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Exceptions;
using ToDoApp.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Task = System.Threading.Tasks.Task;

public class TaskService : ITaskService
{
    private readonly ToDoContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<CreateTaskDto> _createTaskValidator;
    private readonly IValidator<UpdateDescriptionOrNameDto> _updateDescriptionOrNameValidator;
    private readonly IValidator<UpdateStatusDto> _updateStatusValidator;
    private readonly IValidator<UpdateAssigneeDto> _updateAssigneeValidator;

    public TaskService(ToDoContext context,
                       ICurrentUserService currentUserService,
                       IValidator<CreateTaskDto> createTaskValidator,
                       IValidator<UpdateDescriptionOrNameDto> updateDescriptionOrNameValidator,
                       IValidator<UpdateStatusDto> updateStatusValidator,
                       IValidator<UpdateAssigneeDto> updateAssigneeValidator)
    {
        _context = context;
        _currentUserService = currentUserService;
        _createTaskValidator = createTaskValidator;
        _updateDescriptionOrNameValidator = updateDescriptionOrNameValidator;
        _updateStatusValidator = updateStatusValidator;
        _updateAssigneeValidator = updateAssigneeValidator;
    }

    public async Task<List<ToDoApp.Data.Models.Task>> GetAsync()
    {
        return await _context.Tasks
            .Where(x => x.CreatedById == _currentUserService.UserId)
            .ToListAsync();
    }

    public async Task CreateAsync(CreateTaskDto taskDto)
    {
        var validationResult = await _createTaskValidator.ValidateAsync(taskDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var item = new ToDoApp.Data.Models.Task
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

    public async Task UpdateDescriptionOrNameAsync(int taskId, UpdateDescriptionOrNameDto dto)
    {
        var validationResult = await _updateDescriptionOrNameValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var task = await _context.Tasks.FindAsync(taskId);
        if (task != null)
        {
            if (!string.IsNullOrEmpty(dto.Title))
            {
                task.Title = dto.Title;
            }
            if (!string.IsNullOrEmpty(dto.Description))
            {
                task.Description = dto.Description;
            }
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateStatusAsync(int taskId, UpdateStatusDto dto)
    {
        var validationResult = await _updateStatusValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var task = await _context.Tasks
            .Include(t => t.Status)
            .FirstOrDefaultAsync(t => t.Id == taskId);

        if (task == null)
        {
            throw new TaskNotFoundException(taskId);
        }

        var currentStatus = Enum.Parse<ActivityStatus>(task.Status.Name);

        if (!IsValidStatusTransition(currentStatus, dto.Status))
        {
            throw new InvalidStatusTransitionException(currentStatus, dto.Status);
        }

        task.Status.Name = dto.Status.ToString();
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAssigneeAsync(int taskId, UpdateAssigneeDto dto)
    {
        var validationResult = await _updateAssigneeValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var task = await _context.Tasks.FindAsync(taskId);
        if (task != null)
        {
            task.AssigneeId = dto.AssigneeId;
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
