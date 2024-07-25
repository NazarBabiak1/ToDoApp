using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Models;

namespace ToDoApp.Services.Interfaces
{
    public interface IBoardService
    {
        Task<List<Board>> GetAsync();
    }
}
