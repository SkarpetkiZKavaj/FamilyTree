using System.Reflection;
using FamilyTree_BAL.Interfaces;
using FamilyTree_BAL.Services;
using FamilyTree_DAL.EF;
using FamilyTree_DAL.Models;
using FTEntities.IdentityModels.User;
using FTEntities.Interfaces;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PersonContext>();
builder.Services.AddDbContext<IdentityContext>();
builder.Services.AddScoped<IRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Start}/{id?}");

app.Run();