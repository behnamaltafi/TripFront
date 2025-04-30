public interface IExpenseService
{
    Task<ExpenseDTO> Add(AddExpenseDTO expenseDto);
    Task<Expense> Find(int id);
    Task<List<Expense>> FindTripExpenses(int tripId);
    Task Update(int id, UpdateExpenseDTO expenseDto);
    Task Remove(int id);
    Task<List<ExpenseDTO>> FindFamilyExpenses(int tripId, int familyId);
    Task UpdateParticipant(int expenseId, int familyId, int participantCount);

}
