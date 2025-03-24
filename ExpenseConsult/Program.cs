using Asp.Versioning;
using ExpenseWebApi;
using ExpenseServices;
using ExpenseDataAccessLayer;
using Microsoft.OpenApi.Models;
using Asp.Versioning.ApiExplorer;


var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = "/api/auth/login"; 
//    options.AccessDeniedPath = "/api/auth/access-denied";
//});

builder.Services.AddApiVersioning(option =>
{
    option.AssumeDefaultVersionWhenUnspecified = true; //This ensures if client doesn't specify an API version. The default version should be considered. 
    option.DefaultApiVersion = new ApiVersion(1, 0); //This we set the default API version
    option.ReportApiVersions = true; //The allow the API Version information to be reported in the client  in the response header. This will be useful for the client to understand the version of the API they are interacting with.
    option.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options => {
    options.GroupNameFormat = "'v'VVV"; //The say our format of our version number “‘v’major[.minor][-status]”
    options.SubstituteApiVersionInUrl = true; //This will help us to resolve the ambiguity when there is a routing conflict due to routing template one or more end points are same.
});

// Add services from other projects
services.AddDataAccess(builder.Configuration);
services.AddApplicationServices();

services.AddHostedService<BackgroundWorkerService>();

services.AddAuthentication();
services.AddAuthorization();
services.AddControllers();

services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerDoc(description.GroupName, new OpenApiInfo
        {
            Title = $"Expense API {description.ApiVersion}",
            Version = description.ApiVersion.ToString()
        });
    }
});
var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
