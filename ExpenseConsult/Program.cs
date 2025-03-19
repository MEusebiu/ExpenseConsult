using ExpenseConsult;
using ExpenseConsult.Data;
using ExpenseConsult.Initializers;
using ExpenseConsult.Models;
using ExpenseConsult.Repositories;
using ExpenseConsult.Services;
using ExpenseConsult.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<AppDbContext>(options => 
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), schema => schema.SchemaBehavior(MySqlSchemaBehavior.Ignore)));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/api/auth/login"; 
    options.AccessDeniedPath = "/api/auth/access-denied";
});

services.AddSingleton(new ConcurrentDictionary<string, Expense>());
services.AddSingleton(new ConcurrentDictionary<string, Category>());
services.AddSingleton(new ConcurrentDictionary<string, User>());
services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
services.AddTransient<ICategoryService, CategoryService>();
services.AddTransient<IUserService, UserService>();
services.AddTransient<IExpenseService, ExpenseService>();

services.AddHostedService<ExpenseCacheInitializer>();
services.AddHostedService<CategoryCacheInitializer>();

services.AddAuthentication();
services.AddAuthorization();
services.AddControllers();

services.AddEndpointsApiExplorer();

services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("cookieAuth", new OpenApiSecurityScheme
    {
        Name = "Cookie",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Description = "Session-based authentication. Enter your '.AspNetCore.Identity.Application' cookie value."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "cookieAuth"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
