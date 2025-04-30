
// Interfaces



// Implementations

using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
        // Get the trip with families included
        var trip = await _tripRepository.GetByIdAsync(expenseDto.TripId, includeFamilies: true);
        if (trip == null)
            throw new InvalidOperationException("Trip not found");

        // Verify the paying family is part of the trip
        var payingFamily = trip.Families.FirstOrDefault(f => f.FamilyId == expenseDto.FamilyId);
        if (payingFamily == null)
            throw new InvalidOperationException("Paying family not part of this trip");

        // Map and create the expense
        var expense = _mapper.Map<Expense>(expenseDto);
        await _expenseRepository.Add(expense);
        return _mapper.Map<ExpenseDTO>(expense);
    }

    public async Task<Expense> Find(int id)
    {
        return await _expenseRepository.Find(id);
    }

    public async Task<List<Expense>> FindTripExpenses(int tripId)
    {
        return await _expenseRepository.FindTripExpenses(tripId);
    }

    public async Task Update(int id, UpdateExpenseDTO expenseDto)
    {
        await _expenseRepository.Update(id, expenseDto);
    }

    public async Task Remove(int id)
    {
        await _expenseRepository.Remove(id);
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
            throw new InvalidOperationException("Family not part of this trip");

        // Validate against max participants
        if (participantCount > tripFamily.MemberCount)
            throw new InvalidOperationException(
                $"Participant count cannot exceed family's trip participant count ({tripFamily.MemberCount})");

        // Update via repository
        participant.ParticipantCount = participantCount;
        await _expenseRepository.UpdateParticipant(participant);
    }

 

}
