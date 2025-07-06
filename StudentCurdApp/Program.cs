////var builder = WebApplication.CreateBuilder(args);

////// Add services to the container.
////builder.Services.AddRazorPages();

////var app = builder.Build();

////// Configure the HTTP request pipeline.
////if (!app.Environment.IsDevelopment())
////{
////    app.UseExceptionHandler("/Error");
////    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
////    app.UseHsts();
////}

////app.UseHttpsRedirection();
////app.UseStaticFiles();

////app.UseRouting();

////app.UseAuthorization();

////app.MapRazorPages();

////app.Run();

//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using StudentCrudApp.Data;
//using StudentCurdApp.Areas.Identity.Data;
//using StudentCurdApp.Data;
//using StudentCurdApp.Data.Seed;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddRazorPages();

//// Register the DbContext with the connection string from configuration 
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//// Register the Identity services with the custom user class 
//builder.Services.AddDbContext<StudentCurdAppContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// Add Identity services with custom user class and roles
//builder.Services.AddDefaultIdentity<StudentCurdAppUser>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = false;
//})
//.AddRoles<IdentityRole>()
//.AddEntityFrameworkStores<StudentCurdAppContext>();


//var app = builder.Build();

//// SEED 10,000 users only if empty
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    if (!context.Users.Any())
//    {
//        var users = DataSeeder.GenerateUsers(10000);
//        context.Users.AddRange(users);
//        context.SaveChanges();
//    }
//}

//app.UseStaticFiles();
//app.UseRouting();
//app.MapRazorPages();
//app.Run();

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentCrudApp.Data;
using StudentCurdApp.Areas.Identity.Data;
using StudentCurdApp.Data;
using StudentCurdApp.Data.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
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
    // Add password options for development
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<StudentCurdAppContext>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage(); // Show detailed errors in development
}

// IMPORTANT: Create database and seed data in a try-catch block
try
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        // Ensure databases are created
        var appContext = services.GetRequiredService<ApplicationDbContext>();
        var identityContext = services.GetRequiredService<StudentCurdAppContext>();

        // Create databases if they don't exist
        await appContext.Database.EnsureCreatedAsync();
        await identityContext.Database.EnsureCreatedAsync();

        // Alternative: Use migrations instead of EnsureCreated
        // await appContext.Database.MigrateAsync();
        // await identityContext.Database.MigrateAsync();

        // SEED 10,000 users only if empty
        if (!appContext.Users.Any())
        {
            var users = DataSeeder.GenerateUsers(10000);
            appContext.Users.AddRange(users);
            await appContext.SaveChangesAsync();
        }
    }
}
catch (Exception ex)
{
    // Log the error but don't stop the application
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while seeding the database.");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Add Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();