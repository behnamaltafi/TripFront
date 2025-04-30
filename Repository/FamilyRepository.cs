using Microsoft.EntityFrameworkCore;

public class FamilyRepository : IFamilyRepository
{
    private readonly AppDbContext _context;

    public FamilyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Family> GetByIdAsync(int id)
    {
        return await _context.Families.FindAsync(id);
    }

    public async Task<List<Family>> GetAllAsync()
    {
        return await _context.Families.ToListAsync();
    }

    public async Task<Family> AddAsync(Family family)
    {
        await _context.Families.AddAsync(family);
        await _context.SaveChangesAsync();
        return family;
    }

    public async Task UpdateAsync(Family family)
    {
        _context.Families.Update(family);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var family = await GetByIdAsync(id);
        if (family != null)
        {
            _context.Families.Remove(family);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Families.AnyAsync(f => f.Id == id);
    }

   
}
