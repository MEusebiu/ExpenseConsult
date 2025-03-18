using ExpenseConsult;
using ExpenseConsult.Data;
using ExpenseConsult.Initializers;
using ExpenseConsult.Models;
using ExpenseConsult.Repositories;
using ExpenseConsult.Services;
using ExpenseConsult.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), schema => schema.SchemaBehavior(MySqlSchemaBehavior.Ignore)));

services.AddSingleton(new ConcurrentDictionary<int, Expense>());
services.AddSingleton(new ConcurrentDictionary<int, Category>());
services.AddSingleton(new ConcurrentDictionary<int, User>());
services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
services.AddTransient<ICategoryService, CategoryService>();
services.AddTransient<IUserService, UserService>();
services.AddTransient<IExpenseService, ExpenseService>();

services.AddHostedService<ExpenseCacheInitializer>();
services.AddHostedService<CategoryCacheInitializer>();
services.AddHostedService<UserCacheInitializer>();

services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
