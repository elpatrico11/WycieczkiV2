using Microsoft.EntityFrameworkCore;
using WycieczkiV2.Data;
using WycieczkiV2.Repository.Interfaces;
using WycieczkiV2.Repository;
using WycieczkiV2.Services.Interfaces;
using WycieczkiV2.Services;
using FluentValidation;
using WycieczkiV2.Validation;
using WycieczkiV2.ViewModel;
using WycieczkiV2.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext to the service collection
builder.Services.AddDbContext<TripsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

CreateDbIfNotExists(app);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

static void CreateDbIfNotExists(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<TripsContext>();
            DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}
