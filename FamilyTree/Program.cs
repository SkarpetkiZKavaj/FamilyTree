using System.Reflection;
using FamilyTree_BAL.DTO;
using FamilyTree_BAL.Interface;
using FamilyTree_BAL.Services;
using FamilyTree_BAL.Validators;
using FamilyTree_DAL.EF;
using FluentValidation;
using FTEntities.IdentityModels.User;
using FTEntities.Interface;
using FTEntities.UnitOfWotk;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PersonContext>();
builder.Services.AddDbContext<IdentityContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IServiceHub, ServiceHub>();
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.ClientId = configuration["Authentication:Google:ClientId"];
    options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
});
builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/Login");
builder.Services.AddScoped<IValidator<PersonDTO>, PersonDTOValidator>();
builder.Services.AddScoped<IValidator<TreeDTO>, TreeDTOValidator>();
builder.Services.AddScoped<IValidator<DescriptionDTO>, DescriptionDTOValidator>();

var app = builder.Build();

app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Start}/{id?}");

app.Run();