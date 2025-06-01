
// Interfaces



// Implementations

using AutoMapper;
using BusinessExceptionStructure;
using FilterPagingEfCore.Paging;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ITripRepository _tripRepository;
    private readonly IMapper _mapper;


    public ExpenseService(IExpenseRepository expenseRepository,
                         ITripRepository tripRepository,
                         IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _tripRepository = tripRepository;
        _mapper = mapper;
    }

    public async Task<ExpenseDTO> Add(AddExpenseDTO expenseDto)
    {
        var trip = await _tripRepository.GetByIdAsync(expenseDto.TripId, true);
        if (trip == null)
            throw new BusinessException("Trip not found");
        var payingFamily = trip.Families.FirstOrDefault(f => f.FamilyId == expenseDto.FamilyId);
        if (payingFamily == null)
            throw new BusinessException("Paying family not part of this trip");

        // Map and create the expense
        var expense = _mapper.Map<Expense>(expenseDto);
        await _expenseRepository.Add(expense);
        await _expenseRepository.Save();
        return _mapper.Map<ExpenseDTO>(expense);
    }

    public async Task Remove(int id)
    {
        await _expenseRepository.Remove(id);
        await _expenseRepository.Save();
        
    }
    public async Task Update(UpdateExpenseDTO expense)
    {
        await _expenseRepository.Update(expense);
        await _expenseRepository.Save();

    }

    public async Task<PagingResult<Expense>> FindTripExpenses(PagingParam pagingParam, int tripId)
    {
        return await _expenseRepository.FindTripExpenses(pagingParam, tripId);
    }


    public async Task<List<ExpenseDTO>> FindFamilyExpenses(int tripId, int familyId)
    {
        var expenses = await _expenseRepository.FindTripExpenses(tripId);
        return _mapper.Map<List<ExpenseDTO>>(expenses.Where(e => e.FamilyId == familyId));
    }

    public async Task UpdateParticipant(int expenseId, int familyId, int participantCount)
    {
        // Validate participant count
        if (participantCount <= 0)
            throw new ArgumentException("Participant count must be at least 1");

        // Get participant via repository
        var participant = await _expenseRepository.FindParticipant(expenseId, familyId);
        if (participant == null)
            throw new ArgumentException("Participant record not found");

        // Get trip family via repository
        var tripFamily = await _tripRepository.GetTripFamilyAsync(participant.Expense.TripId, familyId);
        if (tripFamily == null)
            throw new BusinessException("Family not part of this trip");

        // Validate against max participants
        if (participantCount > tripFamily.MemberCount)
            throw new BusinessException(
                $"Participant count cannot exceed family's trip participant count ({tripFamily.MemberCount})");

        // Update via repository
        participant.ParticipantCount = participantCount;
        await _expenseRepository.UpdateParticipant(participant);
    }



}
