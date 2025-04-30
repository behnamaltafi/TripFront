

public interface IDebtRecordRepository
{
    Task<DebtRecord> Find(int id);
    Task<string> GetPaymentReceipt(int debtRecordId);
    Task<List<DebtRecordDto>> RegenerateDebtRecords(int tripId, List<DebtRecordDto> computedDebts);
    Task UpdatePaymentReceipt(int DebtRecordId, string base64);
}