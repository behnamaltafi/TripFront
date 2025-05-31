using AutoMapper;
using FilterPagingEfCore.Extenstion;
using FilterPagingEfCore.Filter;
using FilterPagingEfCore.Paging;
using Microsoft.EntityFrameworkCore;

public class ExpenseRepository :  GenericRepository<Expense, int>,IExpenseRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ExpenseRepository(AppDbContext context,
                             IMapper mapper) :base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }



    public async Task<PagingResult<Expense>> FindTripExpenses(PagingParam pagingParam,int tripId)
    {
        return await _context.Expenses
            .Where(e => e.TripId == tripId)
            .Include(e => e.Participants)
            .FilterPaging(pagingParam);
    }
    public async Task<List<Expense>> FindTripExpenses(int tripId)
    {
        return await _context.Expenses
            .Where(e => e.TripId == tripId)
            .Include(e => e.Participants)
            .ToListAsync();
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
