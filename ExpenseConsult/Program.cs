using ExpenseConsult;
using ExpenseConsult.Data;
using ExpenseConsult.Models;
using ExpenseConsult.Repositories;
using ExpenseConsult.Services;
using ExpenseConsult.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

services.AddSingleton(new ConcurrentDictionary<int, Expense>());
services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
services.AddHostedService<ExpenseCacheInitializer>();

services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
