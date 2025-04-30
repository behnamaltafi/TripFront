using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
public class DebtRecordRepository : IDebtRecordRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public DebtRecordRepository(AppDbContext context,
                             IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<DebtRecord> Find(int id)
    {
        return await _context.DebtRecords.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<DebtRecordDto>> RegenerateDebtRecords(int tripId, List<DebtRecordDto> computedDebts)
    {
        var existing = await _context.DebtRecords
            .Where(x => x.TripId == tripId)
            .ToListAsync();

        var unpaid = existing.Where(x => !x.IsPaid).ToList();

        _context.DebtRecords.RemoveRange(unpaid);

        var newRecords = computedDebts
            .Where(d => d.Amount > 0)
            .Select(d => new DebtRecord
            {
                FromFamilyId = d.FromFamilyId,
                ToFamilyId = d.ToFamilyId,
                TripId = tripId,
                Amount = d.Amount,
                AccountNumber = d.AccountNumber,
                IsPaid = false,
                ParticipantCount = d.ParticipantCount
            }).ToList();

        await _context.DebtRecords.AddRangeAsync(newRecords);
        await _context.SaveChangesAsync();
        var result = await _mapper.ProjectTo<DebtRecordDto>(_context.DebtRecords.Where(x => x.TripId == tripId)).ToListAsync();
        return result;
    }
    public async Task UpdatePaymentReceipt(int debtRecordId, string base64)
    {
        var debtRecord = await _context.DebtRecords.FirstOrDefaultAsync(x => x.Id == debtRecordId);
        if (debtRecord == null)
            throw new KeyNotFoundException($"Debt record with ID {debtRecordId} not found.");

        debtRecord.File = base64;
        debtRecord.IsPaid = true;

        await _context.SaveChangesAsync();
    }



    public async Task<string> GetPaymentReceipt(int debtRecordId)
    {
        var debtRecord = await _context.DebtRecords.FirstOrDefaultAsync(x => x.Id == debtRecordId);
        return debtRecord.File;
      
    }
}



