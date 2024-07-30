using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using System;
using System.Collections.Generic;

//var optionsBuilder = new DbContextOptionsBuilder<ToDoContext>();
//var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));
//optionsBuilder.UseMySql("Server=127.0.0.1;Database=ToDoDataBase;User=root;Password=123456n;", serverVersion);

//using (var context = new ToDoContext(optionsBuilder.Options))
//{
//    var user = new User
//    {
//        Id = 1,
//        Name = "Nazar",
//        Tasks = new List<ToDoApp.Data.Models.Task>
//        {
//            new ToDoApp.Data.Models.Task
//            {
//                Id = 1,
//                Title = "Finish Report",
//                Description = "Complete the financial report for Q2",
//                BoardId = 1,
//                CreateAt = DateTime.Now,
//                CreatedById = 1,
//                StatusId = 1,
//                AssigneeId = 2, // Інший користувач
//                User = new User
//                {
//                    Id = 2,
//                    Name = "John Doe"
//                },
//                Board = new Board
//                {
//                    Id = 1,
//                    Name = "Work Board"
//                },
//                Status = new Status
//                {
//                    Id = 1,
//                    Name = "In Progress"
//                }
//            }
//        }
//    };

//    context.Users.Add(user);
//    await context.SaveChangesAsync();
//}
