using FilterPagingEfCore.Paging;

public interface IExpenseService
{
    Task<ExpenseDTO> Add(AddExpenseDTO expenseDto);

    Task<List<ExpenseDTO>> FindFamilyExpenses(int tripId, int familyId);
    Task UpdateParticipant(int expenseId, int familyId, int participantCount);
    Task<PagingResult<Expense>> FindTripExpenses(PagingParam pagingParam, int tripId);
}
