public interface IExpenseRepository:IGenericRepository<Expense,int>
{


    Task<List<Expense>> FindTripExpenses(int tripId);
    Task Update(int id, UpdateExpenseDTO expenseDto);
    Task AddParticipant(ExpenseParticipant participant);
    Task AddRangeParticipant(List<ExpenseParticipant> participant);
    Task<ExpenseParticipant> FindParticipant(int expenseId, int familyId);
    Task UpdateParticipant(ExpenseParticipant participant);

}
