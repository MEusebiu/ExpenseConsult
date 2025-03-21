using ExpenseWebApi;
using ExpenseServices;
using ExpenseDataAccessLayer;


var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = "/api/auth/login"; 
//    options.AccessDeniedPath = "/api/auth/access-denied";
//});

// Add services from other projects
services.AddDataAccess(builder.Configuration);
services.AddApplicationServices();

services.AddHostedService<BackgroundWorkerService>();

services.AddAuthentication();
services.AddAuthorization();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
