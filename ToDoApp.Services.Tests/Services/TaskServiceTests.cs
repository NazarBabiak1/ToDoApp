using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Exceptions;
using ToDoApp.Services.Interfaces;
using ToDoApp.Services.Services;
using FluentValidation;
using FluentValidation.Results;
using Xunit;
using ToDoApp.Data.Enums;
using TaskModel = ToDoApp.Data.Models.Task;

namespace ToDoApp.Service.Tests.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<IValidator<UpdateStatusDto>> _mockValidator;
        private readonly TaskService _taskService;
        private readonly ToDoContext _context;

        public TaskServiceTests()
        {
            _mockValidator = new Mock<IValidator<UpdateStatusDto>>();
            _context = GetDbContext();
            _taskService = new TaskService(_context, null, null, null, _mockValidator.Object, null);
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateStatusAsync_InvalidDto_ThrowsValidationException()
        {
            // Arrange
            var invalidResult = new ValidationResult(new[] { new ValidationFailure("Status", "Invalid status") });
            _mockValidator.Setup(v => v.ValidateAsync(It.IsAny<UpdateStatusDto>(), default)).ReturnsAsync(invalidResult);

            var dto = new UpdateStatusDto { Status = ActivityStatus.Done };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _taskService.UpdateStatusAsync(1, dto));
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateStatusAsync_TaskNotFound_ThrowsTaskNotFoundException()
        {
            // Arrange
            _mockValidator.Setup(v => v.ValidateAsync(It.IsAny<UpdateStatusDto>(), default)).ReturnsAsync(new ValidationResult());

            var dto = new UpdateStatusDto { Status = ActivityStatus.InProgress };

            // Act & Assert
            await Assert.ThrowsAsync<TaskNotFoundException>(() => _taskService.UpdateStatusAsync(1, dto));
        }

        [Theory]
        [InlineData(ActivityStatus.ToDo, ActivityStatus.Done, typeof(InvalidStatusTransitionException))]
        [InlineData(ActivityStatus.Done, ActivityStatus.InProgress, typeof(InvalidStatusTransitionException))]
        public async System.Threading.Tasks.Task UpdateStatusAsync_InvalidTransition_ThrowsInvalidStatusTransitionException(ActivityStatus currentStatus, ActivityStatus newStatus, Type expectedException)
        {
            // Arrange
            _mockValidator.Setup(v => v.ValidateAsync(It.IsAny<UpdateStatusDto>(), default)).ReturnsAsync(new ValidationResult());

            var task = new TaskModel { Id = 1, Status = new Status { Name = currentStatus.ToString() } };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            var dto = new UpdateStatusDto { Status = newStatus };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _taskService.UpdateStatusAsync(1, dto));
            Assert.IsType(expectedException, exception);
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateStatusAsync_ValidTransition_UpdatesStatus()
        {
            // Arrange
            _mockValidator.Setup(v => v.ValidateAsync(It.IsAny<UpdateStatusDto>(), default)).ReturnsAsync(new ValidationResult());

            var task = new TaskModel { Id = 1, Status = new Status { Name = ActivityStatus.ToDo.ToString() } };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            var dto = new UpdateStatusDto { Status = ActivityStatus.InProgress };

            // Act
            await _taskService.UpdateStatusAsync(1, dto);

            // Assert
            var updatedTask = await _context.Tasks.FindAsync(1);
            Assert.Equal(ActivityStatus.InProgress.ToString(), updatedTask.Status.Name);
        }

        private ToDoContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ToDoContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ToDoContext(options);
        }
    }
}
