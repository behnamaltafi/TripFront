
// Interfaces
using FilterPagingEfCore.Paging;
using System;

public interface ITripService
{
    Task<TripDTO> CreateTripAsync(CreateTripDto tripDto);
    Task AddFamilyToTripAsync(int tripId, int familyId, int participantCount);
    Task<PagingResult<TripDTO>> GetTripsForFamilyAsync(PagingParam pagingParam);
    Task<TripDetailsDto> GetTripDetailsAsync(int tripId);
    Task Paid(int tripId);
}
