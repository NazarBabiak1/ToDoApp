using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Services.Services;
using ToDoApp.Services.Interfaces;
using ToDoApp.Middleware; // Додати неймспейс для мідлвари
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using ToDoApp.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ToDoContext>(options =>
{
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));
    options.UseMySql(builder.Configuration.GetConnectionString("ToDoAppDb"), serverVersion);
});

builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserSevice>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
