using System.Reflection;
using System.Security.Claims;
using FamilyTree_BAL.Interface;
using FamilyTree_BAL.Services;
using FamilyTree_DAL.EF;
using FamilyTree_DAL.Models;
using FTEntities.IdentityModels.User;
using FTEntities.Interface;
using FTEntities.UnitOfWotk;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PersonContext>();
builder.Services.AddDbContext<IdentityContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IServiceHub, ServiceHub>();
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/Login");

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Start}/{id?}");

app.Run();