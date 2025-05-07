// Interfaces
public interface ITripRepository:IGenericRepository<Trip, int>
{
    Task<List<Trip>> GetTripsForFamilyAsync();
    Task AddFamilyToTripAsync(int tripId, int familyId, int participantCount);
    Task<TripDetailsDto> GetTripDetailsAsync(int tripId);
    Task Paid(int tripId);
    Task<TripFamily> GetTripFamilyAsync(int tripId, int familyId);
    Task<Trip> GetByIdAsync(int id, bool includeFamilies = false);
}
