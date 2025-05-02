public interface IFamilyRepository
{
    Task<Family> GetByIdAsync(int id);
    Task<List<Family>> GetAllAsync();
    Task<Family> AddAsync(Family family);
    Task UpdateAsync(Family family);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<Family> GetByUserIdAsync(string userId);
}

