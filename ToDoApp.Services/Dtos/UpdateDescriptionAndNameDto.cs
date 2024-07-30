using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Services.Dtos;

public class UpdateDescriptionOrNameDto
{
    public string Title { get; set; }
    public string Description { get; set; }
}
