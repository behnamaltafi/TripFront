
using AutoMapper;
using FilterPagingEfCore.Extenstion;
using FilterPagingEfCore.Paging;
using Microsoft.EntityFrameworkCore;
public class TripRepository : GenericRepository<Trip, int>, ITripRepository
{
    private readonly AppDbContext _context;
    private readonly IFriendshipService _friendshipService;
    private readonly IMapper _mapper;

    public TripRepository(AppDbContext context, IFriendshipService friendshipService, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _friendshipService = friendshipService;
        _mapper = mapper;
    }
    public async Task<TripFamily> GetTripFamilyAsync(int tripId, int familyId)
    {
        return await _context.TripFamilies
            .FirstOrDefaultAsync(tf => tf.TripId == tripId && tf.FamilyId == familyId);
    }
    public async Task<PagingResult<TripFamilyDto>> GetTripFamilyAsync(PagingParam pagingParam, int tripId)
    {
        var qry = _context.TripFamilies.Include(tf => tf.Family)
           .Where(tf => tf.TripId == tripId);
        return await _mapper.ProjectTo<TripFamilyDto>(qry).FilterPaging(pagingParam);
    }

    public async Task<TripDetailsDto> GetTripDetailsAsync(int tripId)
    {
        var trip = _context.Trips
            .Include(t => t.Families).ThenInclude(tf => tf.Family)
            .Include(t => t.Expenses)
                .ThenInclude(e => e.Participants)
                    .ThenInclude(p => p.Family)
            .Where(t => t.Id == tripId);
        var dto = await _mapper.ProjectTo<TripDetailsDto>(trip).FirstOrDefaultAsync();

        // Calculate shares and balances
        var totalParticipants = dto.TotalParticipants;
        if (totalParticipants > 0)
        {
            var sharePerParticipant = dto.TotalExpenses / totalParticipants;

            foreach (var family in dto.TripFamilyDetails)
            {
                // Calculate total paid by this family
                family.TotalPaid = dto.ExpensesDetails
                    .Where(e => e.FamilyId == family.FamilyId)
                    .Sum(e => e.Amount);

                // Calculate their fair share
                family.TotalShare = sharePerParticipant * family.MemberCount;

                // Calculate balance
                family.Balance = family.TotalPaid - family.TotalShare;
            }

            // Calculate share amounts for each expense participant
            foreach (var expense in dto.ExpensesDetails)
            {
                var totalExpenseParticipants = expense.Participants.Sum(p => p.ParticipantCount);
                if (totalExpenseParticipants > 0)
                {
                    var sharePerExpenseParticipant = expense.Amount / totalExpenseParticipants;
                    foreach (var participant in expense.Participants)
                    {
                        participant.ShareAmount = sharePerExpenseParticipant * participant.ParticipantCount;
                    }
                }
            }
        }

        return dto;
    }
    public async Task<Trip> GetByIdAsync(int id, bool includeFamilies = false)
    {
        var query = _context.Trips.AsQueryable();

        if (includeFamilies)
        {
            query = query.Include(t => t.Families).ThenInclude(x => x.Family).Include(t => t.Expenses);
        }

        return await query.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Trip>> GetTripsForFamilyAsync()
    {
        var familyId = _friendshipService.GetFamilyId();
        return await _context.Trips
            .Where(t => t.Families.Any(f => f.FamilyId == familyId))
            .Include(t => t.Families).ThenInclude(tf => tf.Family)
            .Include(t => t.Expenses).ThenInclude(e => e.Participants)
            .ToListAsync();
    }





    public async Task AddFamilyToTripAsync(TripFamily tripFamily)
    {
        var trip = await _context.Trips.FirstOrDefaultAsync(t => t.Id == tripFamily.TripId);
        if (trip == null)
        {
            throw new KeyNotFoundException();
        }

        await _context.TripFamilies.AddAsync(tripFamily);
        await _context.SaveChangesAsync();
    }

    public async Task Paid(int tripId)
    {
        var trip = await _context.Trips.FindAsync(tripId);
        trip.IsPaid = true;
        _context.Trips.Update(trip);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTripFamily(TripFamily tripFamily)
    {


        _context.TripFamilies.Update(tripFamily);
        await _context.SaveChangesAsync();
    }
    public async Task RemoveTripFamily(int tripFamilyId)
    {
        var tripFamily = await _context.TripFamilies.Where(x=>x.Id ==tripFamilyId &&x.Trip.OwnerFamilyId!=x.FamilyId).FirstOrDefaultAsync();
        _context.TripFamilies.Remove(tripFamily);
        await _context.SaveChangesAsync();
    }
}
