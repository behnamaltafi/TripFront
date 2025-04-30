using AutoMapper;

public class DebtRecordService : IDebtRecordService
{

    private readonly IDebtRecordRepository _debtRecordRepository;
    private readonly IMapper _mapper;

    public DebtRecordService(IMapper mapper, IDebtRecordRepository debtRecordRepository)
    {
        _mapper = mapper;
        _debtRecordRepository = debtRecordRepository;
    }
    public async Task<DebtRecord> Find(int id)
    {
        return await _debtRecordRepository.Find(id);
    }

    public async Task<List<DebtRecordDto>> RegenerateDebtRecords(int tripId, List<DebtRecordDto> computedDebts)
    {
        return await _debtRecordRepository.RegenerateDebtRecords(tripId, computedDebts);
    }
    public async Task UpdatePaymentReceipt(int debtRecordId, string base64)
    {
        await _debtRecordRepository.UpdatePaymentReceipt(debtRecordId, base64);
    }
    public async Task<string> GetPaymentReceipt(int debtRecordId)
    {
        return await _debtRecordRepository.GetPaymentReceipt(debtRecordId);
    }
}


