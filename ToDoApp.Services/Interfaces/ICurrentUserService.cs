﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Services.Interfaces
{
    public interface ICurrentUserService
    {
       int UserId{ get; }
    }
}