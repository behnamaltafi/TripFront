
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
    public async Task<PagingResult<TripFamilyDto>> GetTripFamilyAsync(PagingParam pagingParam, int tripId)
    {
        return await _tripRepository.GetTripFamilyAsync(pagingParam, tripId);
    }

    public async Task<TripDTO> CreateTripAsync(CreateTripDto tripDto)
    {
        var trip = _mapper.Map<Trip>(tripDto);
        var familyId = _friendshipService.GetFamilyId();
        trip.OwnerFamily = familyId;
        await _tripRepository.Add(trip);
        await _tripRepository.Save();
        var tripFamily = new TripFamily
        {
            FamilyId = familyId,
            TripId = trip.Id,
            MemberCount = tripDto.MemberCount,
        };
        await _tripRepository.AddFamilyToTripAsync(tripFamily);
        return _mapper.Map<TripDTO>(trip);
    }

    public async Task<TripDetailsDto> GetTripDetailsAsync(int tripId)
    {
        return await _tripRepository.GetTripDetailsAsync(tripId);
    }
    public async Task<TripDetailsDto> GetTrip(int tripId)
    {
        return await _tripRepository.GetTripDetailsAsync(tripId);
    }
    public async Task AddFamilyToTripAsync(TripFamily tripFamily)
    {
        await _tripRepository.AddFamilyToTripAsync(tripFamily);
    }
    public async Task UpdateTripFamily(TripFamily tripFamily)
    {
        await _tripRepository.UpdateTripFamily(tripFamily);
    }
    public async Task RemoveTripFamily(int tripFamilyId)
    {
        var familyId = _friendshipService.GetFamilyId();

        await _tripRepository.RemoveTripFamily(tripFamilyId);
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
        await _tripRepository.Save();
    }
    public async Task Update(UpdateTripDto updateTripDto)
    {
        await _tripRepository.Update(updateTripDto);
    }
}
