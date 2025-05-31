
using AutoMapper;
using FilterPagingEfCore.Paging;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;
    private readonly IFriendshipService _friendshipService;
    private readonly IMapper _mapper;

    public TripService(ITripRepository tripRepository, IMapper mapper, IFriendshipService friendshipService)
    {
        _tripRepository = tripRepository;
        _mapper = mapper;
        _friendshipService = friendshipService;
    }

    public async Task<TripDTO> CreateTripAsync(CreateTripDto tripDto)
    {
        var trip = _mapper.Map<Trip>(tripDto);
        var familyId = _friendshipService.GetFamilyId();
        trip.OwnerFamily = familyId;
        await _tripRepository.Add(trip);
        await _tripRepository.Save();
        await _tripRepository.AddFamilyToTripAsync(trip.Id, familyId, tripDto.MemberCount);
        return _mapper.Map<TripDTO>(trip);
    }

    public async Task<TripDetailsDto> GetTripDetailsAsync(int tripId)
    {
        return await _tripRepository.GetTripDetailsAsync(tripId);
    }
    public async Task AddFamilyToTripAsync(int tripId, int familyId, int participantCount)
    {
        await _tripRepository.AddFamilyToTripAsync(tripId, familyId, participantCount);
    }

    public async Task<PagingResult<TripDTO>> GetTripsForFamilyAsync(PagingParam pagingParam)
    {
        var familyId = _friendshipService.GetFamilyId();
        var trips = await _tripRepository.FindAllPaging<TripDTO>(pagingParam, x => x.Families.Any(f => f.FamilyId == familyId));

        return trips;
    }
    public async Task Paid(int tripId)
    {
        await _tripRepository.Paid(tripId);
    }
    public async Task Delete(int tripId)
    {
        await _tripRepository.Remove(tripId);
    }
    public async Task Update(UpdateTripDto updateTripDto)
    {
        await _tripRepository.Update(updateTripDto);
    }
}
