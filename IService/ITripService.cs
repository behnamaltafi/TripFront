
// Interfaces
using System;

public interface ITripService
{
    Task<TripDTO> CreateTripAsync(CreateTripDto tripDto);
    Task AddFamilyToTripAsync(int tripId, int familyId, int participantCount);
    Task<List<TripDTO>> GetTripsForFamilyAsync();
    Task<TripDetailsDto> GetTripDetailsAsync(int tripId);
    Task Paid(int tripId);
}
