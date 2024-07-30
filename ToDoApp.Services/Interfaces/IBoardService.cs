using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using Task = System.Threading.Tasks.Task;

namespace ToDoApp.Services.Interfaces
{
    public interface IBoardService
    {
        Task<List<Board>> GetAsync();
        Task CreateBoardAsync(CreateBoardDto taskDto);
    }
}
