using FilterPagingEfCore.Paging;

public interface IExpenseRepository:IGenericRepository<Expense,int>
{


    Task AddParticipant(ExpenseParticipant participant);
    Task AddRangeParticipant(List<ExpenseParticipant> participant);
    Task<ExpenseParticipant> FindParticipant(int expenseId, int familyId);
    Task<PagingResult<Expense>> FindTripExpenses(PagingParam pagingParam, int tripId);
    Task<List<Expense>> FindTripExpenses(int tripId);
    Task UpdateParticipant(ExpenseParticipant participant);
}
