using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class ExpenseRepository : IExpenseRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ExpenseRepository(AppDbContext context,
                             IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Add(Expense expense)
    {
        await _context.Expenses.AddAsync(expense);
        await _context.SaveChangesAsync();
    }

    public async Task<Expense> Find(int id)
    {
        return await _context.Expenses.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Expense>> FindTripExpenses(int tripId)
    {
        return await _context.Expenses
            .Where(e => e.TripId == tripId)
            .Include(e => e.Participants)
            .ToListAsync();
    }

    public async Task Update(int id, UpdateExpenseDTO expenseDto)
    {
        var expense = await _context.Expenses.FindAsync(id);
        if (expense == null)
        {
            // Optionally handle "not found" case
            throw new KeyNotFoundException($"Expense with Id {id} not found.");
        }

        expense.Amount = expenseDto.Amount;
        expense.Date = expenseDto.Date;
        expense.Description = expenseDto.Description;

        _context.Expenses.Update(expense);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);
        if (expense == null)
        {
            // Optionally handle "not found" case
            throw new KeyNotFoundException($"Expense with Id {id} not found.");
        }

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
    }
    public async Task AddParticipant(ExpenseParticipant participant)
    {
        await _context.ExpenseParticipants.AddAsync(participant);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeParticipant(List<ExpenseParticipant> participant)
    {
        await _context.ExpenseParticipants.AddRangeAsync(participant);
        await _context.SaveChangesAsync();
    }

    public async Task<ExpenseParticipant> FindParticipant(int expenseId, int familyId)
    {
        return await _context.ExpenseParticipants
            .Include(ep => ep.Expense)
            .FirstOrDefaultAsync(ep => ep.ExpenseId == expenseId && ep.FamilyId == familyId);
    }

    public async Task UpdateParticipant(ExpenseParticipant participant)
    {
        _context.ExpenseParticipants.Update(participant);
        await _context.SaveChangesAsync();
    }

}
