using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WycieczkiV2.Data;
using WycieczkiV2.Models;

public static class DbInitializer
{
    public static async Task InitializeAsync(TripsContext context)
    {
        await context.Database.EnsureCreatedAsync();

        // Look for any students.
        if (await context.Students.AnyAsync())
        {
            return;   // DB has been seeded
        }

        var students = new Student[]
        {
            new Student{FirstName="Carson",LastName="Alexander",DateOfBirth=DateTime.Parse("2005-09-01"), PhoneNumber = "123-456-7890", Email = "carson@example.com", Citizenship = "USA"},
            new Student{FirstName="Meredith",LastName="Alonso",DateOfBirth=DateTime.Parse("2002-09-01"), PhoneNumber = "123-456-7890", Email = "meredith@example.com", Citizenship = "UK"},
            new Student{FirstName="Arturo",LastName="Anand",DateOfBirth=DateTime.Parse("2003-09-01"), PhoneNumber = "123-456-7890", Email = "arturo@example.com", Citizenship = "Mexico"},
            new Student{FirstName="Gytis",LastName="Barzdukas",DateOfBirth=DateTime.Parse("2002-09-01"), PhoneNumber = "123-456-7890", Email = "gytis@example.com", Citizenship = "Lithuania"},
            new Student{FirstName="Yan",LastName="Li",DateOfBirth=DateTime.Parse("2002-09-01"), PhoneNumber = "123-456-7890", Email = "yan@example.com", Citizenship = "China"},
            new Student{FirstName="Peggy",LastName="Justice",DateOfBirth=DateTime.Parse("2001-09-01"), PhoneNumber = "123-456-7890", Email = "peggy@example.com", Citizenship = "Canada"},
            new Student{FirstName="Laura",LastName="Norman",DateOfBirth=DateTime.Parse("2003-09-01"), PhoneNumber = "123-456-7890", Email = "laura@example.com", Citizenship = "Australia"},
            new Student{FirstName="Nino",LastName="Olivetto",DateOfBirth=DateTime.Parse("2005-09-01"), PhoneNumber = "123-456-7890", Email = "nino@example.com", Citizenship = "Italy"}
        };

        foreach (Student s in students)
        {
            context.Students.Add(s);
        }
        await context.SaveChangesAsync();

        var trips = new Trip[]
        {
            new Trip{Name="Trip 1",Date=DateTime.Parse("2021-09-01"),Price=Decimal.Parse("100"), Origin="Origin 1", Destination="Destination 1", Country="Country 1"},
            new Trip{Name="Trip 2",Date=DateTime.Parse("2021-09-01"),Price=Decimal.Parse("200"), Origin="Origin 2", Destination="Destination 2", Country="Country 2"},
            new Trip{Name="Trip 3",Date=DateTime.Parse("2021-09-01"),Price=Decimal.Parse("300"), Origin="Origin 3", Destination="Destination 3", Country="Country 3"},
            new Trip{Name="Trip 4",Date=DateTime.Parse("2021-09-01"),Price=Decimal.Parse("400"), Origin="Origin 4", Destination="Destination 4", Country="Country 4"},
            new Trip{Name="Trip 5",Date=DateTime.Parse("2021-09-01"),Price=Decimal.Parse("500"), Origin="Origin 5", Destination="Destination 5", Country="Country 5"}
        };

        foreach (Trip c in trips)
        {
            context.Trips.Add(c);
        }
        await context.SaveChangesAsync();

        // Get updated entities with changed IDs from the database
        var updatedStudents = await context.Students.ToListAsync();
        var updatedTrips = await context.Trips.ToListAsync();

        var reservations = new Reservation[]
        {
            new Reservation{StudentId=updatedStudents[0].StudentId,TripId=updatedTrips[0].TripId,DateOfReservation=DateTime.Parse("2021-09-01"),StartDate=DateTime.Parse("2021-09-01"),EndDate=DateTime.Parse("2021-09-05"),TotalPrice=Decimal.Parse("100"),NumberOfPeople=1,PaymentDate=null},
            new Reservation{StudentId=updatedStudents[1].StudentId,TripId=updatedTrips[2].TripId,DateOfReservation=DateTime.Parse("2021-09-01"),StartDate=DateTime.Parse("2021-09-02"),EndDate=DateTime.Parse("2021-09-07"),TotalPrice=Decimal.Parse("200"),NumberOfPeople=2,PaymentDate=null},
            new Reservation{StudentId=updatedStudents[1].StudentId,TripId=updatedTrips[1].TripId,DateOfReservation=DateTime.Parse("2021-09-01"),StartDate=DateTime.Parse("2021-09-03"),EndDate=DateTime.Parse("2021-09-08"),TotalPrice=Decimal.Parse("300"),NumberOfPeople=3,PaymentDate=null},
            new Reservation{StudentId=updatedStudents[2].StudentId,TripId=updatedTrips[1].TripId,DateOfReservation=DateTime.Parse("2021-09-01"),StartDate=DateTime.Parse("2021-09-04"),EndDate=DateTime.Parse("2021-09-09"),TotalPrice=Decimal.Parse("400"),NumberOfPeople=4,PaymentDate=null}
        };

        foreach (Reservation reservation in reservations)
        {
            context.Reservations.Add(reservation);
        }
        await context.SaveChangesAsync();
    }
}
