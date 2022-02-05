using System.Globalization;
using System.Reflection;
using FluentValidation.AspNetCore;
using MediatR;
using MediatRFluentDemo.Context;
using MediatRFluentDemo.PipelineBehaviours;
using MediatRFluentDemo.Settings;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

#region Service
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region DbContext
builder.Services.AddDbContext<ApplicationContext>(
               opt =>
               {
                   opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                   opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
               });
builder.Services.AddScoped<IApplicationContext>(provider => provider.GetService<ApplicationContext>());
#endregion

#region Swagger
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.SwaggerDoc("DevBGDemo",
        // we can set properties related to additional information
        new OpenApiInfo()
        {
            Version = "v1",
            Title = "MediatRFluentValidationDemo API",
            Description = "some description of the API",
        });
});
#endregion

#region MediatR
// TODO:[MediatR][Step 1]: Register MediatR assemblies
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection("CacheSettings"));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));  
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>)); 
#endregion

#region FluentValidaiton
builder.Services.AddControllers()
     .AddFluentValidation(s =>
     {
         s.RegisterValidatorsFromAssemblyContaining<Program>(); // register all validators from assembly.
         s.DisableDataAnnotationsValidation = true; // use fluent validation instead of Data annotations.
     });

builder.Services.AddLocalization(opt => opt.ResourcesPath = "Resources");
#endregion

#endregion

#region Middlewares
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setupAction =>
    {
        setupAction.DisplayRequestDuration();
        setupAction.SwaggerEndpoint("/swagger/DevBGDemo/swagger.json", "demo API");
    });
}

app.UseRequestLocalization(GetLocalizationOptions());
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion

static RequestLocalizationOptions GetLocalizationOptions()
{
    var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("fr-FR"),
        new CultureInfo("en-GB"),
        new CultureInfo("bg-BG")
    };

    var options = new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture("bg-BG"),
        SupportedCultures = supportedCultures,
        SupportedUICultures = supportedCultures
    };

    return options;
}