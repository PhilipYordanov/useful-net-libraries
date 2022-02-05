using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniProfilerDemo.Data;
using MiniProfilerDemo.Services;

# region Services
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ITagService, TagService>();

builder.Services.AddHttpClient();
// TODO:[MiniProfiler][Step 1]: Register Mini profiler
builder.Services.AddMiniProfiler(options =>
{
    options.PopupShowTimeWithChildren = true; // <-- more detailed information
    options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.BottomLeft; // <-- position of popups
    //options.ResultsAuthorize = (request) => request.HttpContext.User.IsInRole("Developer"); <-- claims; role
    //options.UserIdProvider = (request) => request.HttpContext.User.Identity.Name; // <-- based on name
    //options.Storage = new SqlServerStorage(get it with Configuration.GetConnectionString("cs")); <-- store data from logging
    //options.PopupStartHidden = true; // show popup with 'alt + p'
})
.AddEntityFramework();
#endregion

# region Middlewares
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// TODO:[MiniProfiler][Step 2]: add miniprofiler middleware to profile our HTTP requests
app.UseMiniProfiler();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
#endregion