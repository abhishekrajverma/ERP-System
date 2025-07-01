//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddRazorPages();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapRazorPages();

//app.Run();

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentCrudApp.Data;
using StudentCurdApp.Areas.Identity.Data;
using StudentCurdApp.Data;
using StudentCurdApp.Data.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Register the DbContext with the connection string from configuration 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Register the Identity services with the custom user class 
builder.Services.AddDbContext<StudentCurdAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity services with custom user class and roles
builder.Services.AddDefaultIdentity<StudentCurdAppUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<StudentCurdAppContext>();


var app = builder.Build();

// SEED 10,000 users only if empty
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (!context.Users.Any())
    {
        var users = DataSeeder.GenerateUsers(10000);
        context.Users.AddRange(users);
        context.SaveChanges();
    }
}

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();
