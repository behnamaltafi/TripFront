

public interface IDebtRecordRepository:IGenericRepository<DebtRecord, int>
{
    Task<List<DebtRecordDto>> RegenerateDebtRecords(int tripId, List<DebtRecordDto> computedDebts);
    Task UpdatePaymentReceipt(int DebtRecordId, string base64);
}