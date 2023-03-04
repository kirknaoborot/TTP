using Microsoft.EntityFrameworkCore;
using TTP.Domain;
using Microsoft.AspNetCore.Identity;
using TTP.Settings;
using System.Drawing.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("TTPConnection")));

var config = new AdminSettings();
builder.Configuration.Bind("AdminSettings", config);
builder.Services.AddSingleton(config);


var defaultEntity = new DefaultEntity(builder.Configuration);
await defaultEntity.AddDefultEntity();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
