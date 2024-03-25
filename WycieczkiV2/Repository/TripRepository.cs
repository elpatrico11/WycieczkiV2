using Microsoft.EntityFrameworkCore;
using WycieczkiV2.Data;
using WycieczkiV2.Models;
using WycieczkiV2.Repository.Interfaces;

namespace WycieczkiV2.Repository
{
    public class TripRepository : ITripRepository
    {
        private readonly TripsContext _context;
        public TripRepository(TripsContext context)
        {
            _context = context;
        }
        public IEnumerable<Trip> GetAll()
        {
            return _context.Trips.ToList();
        }
  
        public Trip? GetById(int TripId)
        {
            return _context.Trips.Find(TripId);
        }
        public void Insert(Trip trip)
        {
            _context.Trips.Add(trip);
        }
 
        public void Update(Trip trip)
        {

            _context.Entry(trip).State = EntityState.Modified;
        }
    
        public void Delete(int TripId)
        {
            //First, fetch the Employee details based on the EmployeeID id
            Trip? trip = _context.Trips.Find(TripId);
            //If the employee object is not null, then remove the employee
            if (trip != null)
            {
                //This will mark the Entity State as Deleted
                _context.Trips.Remove(trip);
            }

        }
        //This method will make the changes permanent in the database
        //That means once we call Insert, Update, and Delete Methods, then we need to call
        //the Save method to make the changes permanent in the database
        public void Save()
        {
            //Based on the Entity State, it will generate the corresponding SQL Statement and
            //Execute the SQL Statement in the database
            //For Added Entity State: It will generate INSERT SQL Statement
            //For Modified Entity State: It will generate UPDATE SQL Statement
            //For Deleted Entity State: It will generate DELETE SQL Statement
            _context.SaveChanges();
        }
        private bool disposed = false;
        //As a context object is a heavy object or you can say time-consuming object
        //So, once the operations are done we need to dispose of the same using Dispose method
        //The EmployeeDBContext class inherited from DbContext class and the DbContext class
        //is Inherited from the IDisposable interface
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
