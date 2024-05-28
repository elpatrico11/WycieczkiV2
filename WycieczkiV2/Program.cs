using Microsoft.EntityFrameworkCore;
using WycieczkiV2.Data;
using WycieczkiV2.Repository.Interfaces;
using WycieczkiV2.Repository;
using WycieczkiV2.Services.Interfaces;
using WycieczkiV2.Services;
using System.Threading.Tasks;
using FluentValidation;
using WycieczkiV2.Validation;
using WycieczkiV2.ViewModel;
using WycieczkiV2.AutoMapper;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add DbContext to the service collection
builder.Services.AddDbContext<TripsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<TripsContext>();




builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

//Add automapper

builder.Services.AddAutoMapper(options =>
{
    options.AddProfile<Mapper>();
});


// Add validators
builder.Services.AddScoped<IValidator<StudentViewModel>, StudentValidator>();
builder.Services.AddScoped<IValidator<TripViewModel>, TripValidator>();
builder.Services.AddScoped<IValidator<ReservationViewModel>, ReservationValidator>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

await CreateDbIfNotExistsAsync(app);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

async Task CreateDbIfNotExistsAsync(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<TripsContext>();
            await DbInitializer.InitializeAsync(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}

using (var scope = app.Services.CreateScope())
{
   var roleManager = 
        scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] {"Admin", "Manager", "User"}; 
    
    foreach(var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
            Console.WriteLine($"Role '{role}' created successfully.");
        }
        else
        {
            Console.WriteLine($"Role '{role}' already exists.");
        }


           
     
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string email = "admin@admin.com";
    string password = "Test1234!";
    

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser();
        user.UserName = email;
        user.Email = email;
        user.EmailConfirmed = true;

        var result = await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");
    }

      
  
};

    app.Run();


