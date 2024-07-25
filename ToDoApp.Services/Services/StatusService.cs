using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Services;

public class StatusService : IStatusService
{
    private ToDoContext _context;

    public StatusService(ToDoContext context)
    {
        _context = context;
    }
    public async Task<List<Status>> GetAsync()
    {
        return await _context.Statuses.ToListAsync();
    }
}
