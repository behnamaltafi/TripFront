
// Interfaces
using FilterPagingEfCore.Paging;
using System;

public interface ITripService
{
    Task<TripDTO> CreateTripAsync(CreateTripDto tripDto);
    Task<PagingResult<TripDTO>> GetTripsForFamilyAsync(PagingParam pagingParam);
    Task<TripDetailsDto> GetTripDetailsAsync(int tripId);
    Task Paid(int tripId);
    Task Delete(int tripId);
    Task Update(UpdateTripDto updateTripDto);
    Task<PagingResult<TripFamilyDto>> GetTripFamilyAsync(PagingParam pagingParam, int tripId);
    Task AddFamilyToTripAsync(TripFamily tripFamily);
    Task UpdateTripFamily(TripFamily tripFamily);
    Task RemoveTripFamily(int tripFamilyId);
}
