using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Enums;
using ToDoApp.Data.Models;

namespace ToDoApp.Services.Dtos;

public class UpdateStatusDto
{
    public ActivityStatus Status { get; set; }
}
