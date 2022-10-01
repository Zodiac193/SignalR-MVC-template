
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SignalR_Sample1.Data;
using SignalR_Sample1.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

//var connectionAzureSignalR = "Endpoint=https://signalrsampleapp.service.signalr.net;AccessKey=MfOgFJ/+f+BwWyOF75Jxe6zfTZoSF8teC0mvSjmNGyQ=;Version=1.0;";

builder.Services.AddSignalR();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapHub<UserHub>("hubs/userCount");
app.MapHub<DeathlyHallowsHub>("hubs/deathlyHallows");
app.MapHub<HouseGroupHub>("hubs/houseGroup");
app.MapHub<NotificationHub>("hubs/notificationHub");
app.MapHub<ChatHub>("hubs/Chat");
app.MapHub<OrderHub>("hubs/order");


app.Run();
