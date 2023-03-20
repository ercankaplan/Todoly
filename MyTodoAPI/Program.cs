using Microsoft.Extensions.Configuration;
using MyTodo.Data.Models.Ef.EfContext;
using MyTodoAPI.Poviders;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Get environment configurations

var configuration = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

builder.Services.AddDbContext<TODOLYDbContext>(options => {
    options.UseNpgsql(configuration["ConnectionStrings:ConnStr"], b => b.MigrationsAssembly("MyTodo.Data"));
});
builder.Services.AddScoped<ITodoItemProvider, TodoItemProvider>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
