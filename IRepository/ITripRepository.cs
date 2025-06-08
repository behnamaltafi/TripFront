// Interfaces
using FilterPagingEfCore.Paging;

public interface ITripRepository:IGenericRepository<Trip, int>
{
    Task<List<Trip>> GetTripsForFamilyAsync();
    Task<TripDetailsDto> GetTripDetailsAsync(int tripId);
    Task Paid(int tripId);
    Task<TripFamily> GetTripFamilyAsync(int tripId, int familyId);
    Task<Trip> GetByIdAsync(int id, bool includeFamilies = false);
    Task<PagingResult<TripFamilyDto>> GetTripFamilyAsync(PagingParam pagingParam, int tripId);
    Task AddFamilyToTripAsync(TripFamily tripFamily);
    Task UpdateTripFamily(TripFamily tripFamily);
    Task RemoveTripFamily(int tripFamilyId);
}
