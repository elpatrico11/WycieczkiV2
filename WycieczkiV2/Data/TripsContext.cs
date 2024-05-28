namespace WycieczkiV2.Data;
using Microsoft.EntityFrameworkCore;
using WycieczkiV2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class TripsContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public TripsContext(DbContextOptions<TripsContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Trip>().ToTable("Trips");
        modelBuilder.Entity<Student>().ToTable("Students");
        modelBuilder.Entity<Reservation>().ToTable("Reservations");
    }
}