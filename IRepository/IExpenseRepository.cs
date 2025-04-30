public interface IExpenseRepository
{
    Task Add(Expense expense);
    Task<Expense> Find(int id);
    Task<List<Expense>> FindTripExpenses(int tripId);
    Task Update(int id, UpdateExpenseDTO expenseDto);
    Task Remove(int id);
    Task AddParticipant(ExpenseParticipant participant);
    Task AddRangeParticipant(List<ExpenseParticipant> participant);
    Task<ExpenseParticipant> FindParticipant(int expenseId, int familyId);
    Task UpdateParticipant(ExpenseParticipant participant);

}
