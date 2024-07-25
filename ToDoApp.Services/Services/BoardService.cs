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

public class BoardService : IBoardService
{
    private ToDoContext _context;

    public BoardService(ToDoContext context)
    {
        _context = context;
    }
    public async Task<List<Board>> GetAsync()
    {
        return await _context.Boards.ToListAsync();
    }
}
