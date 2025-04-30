// Interfaces
public interface ITripRepository
{

    Task<List<Trip>> GetTripsForFamilyAsync();
    Task AddAsync(Trip trip);
    Task UpdateAsync(Trip trip);
    Task AddFamilyToTripAsync(int tripId, int familyId, int participantCount);
    Task<Trip> GetByIdAsync(int id, bool includeFamilies = false);
    Task<TripFamily> GetTripFamilyAsync(int tripId, int familyId);
    Task<TripDetailsDto> GetTripDetailsAsync(int tripId);
    Task Paid(int tripId);
}
