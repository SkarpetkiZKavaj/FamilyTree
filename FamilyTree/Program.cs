using System.Reflection;
using FamilyTree_BAL.Interfaces;
using FamilyTree_BAL.Services;
using FamilyTree_DAL.EF;
using FamilyTree_DAL.Models;
using FTEntities.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PersonContext>();
builder.Services.AddScoped<IRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();

var app = builder.Build();

app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();