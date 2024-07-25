using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Context;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Services;

public class TaskService : ITaskService
{
    private ToDoContext _context;

    public TaskService(ToDoContext context)
    {
        _context = context;
    }

    public async Task<List<Data.Models.Task>> GetAsync()
    {
        return await _context.Tasks.ToListAsync();
    }
}
