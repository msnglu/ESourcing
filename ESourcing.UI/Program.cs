

using ESourcing.Core.Entities;
using ESourcing.Core.Repositories;
using ESourcing.Core.Repositories.Base;
using ESourcing.Infrastructure.Data;
using ESourcing.Infrastructure.Repository;
using ESourcing.Infrastructure.Repository.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>) );
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
var connectionString = builder.Configuration.GetConnectionString("IdentityConnection");
builder.Services.AddDbContext<WebAppContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 4;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireDigit = false;

}).AddEntityFrameworkStores<WebAppContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Home/Login";
    options.LogoutPath = $"/Home/Logout";
});
var app = builder.Build();
CreateAndSeedDatabase(app);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});
app.Run();

static void CreateAndSeedDatabase(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();

        try
        {
            var aspnetRunContext = services.GetRequiredService<WebAppContext>();
            WebAppContextSeed.SeedAsync(aspnetRunContext, loggerFactory).Wait();
        }
        catch (Exception exception)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(exception, "An error occured seeding the DB");
        }
    }
}