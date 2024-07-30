using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Services;

public class CurrentUserSevice : ICurrentUserService
{
    public int UserId => 1;
}
