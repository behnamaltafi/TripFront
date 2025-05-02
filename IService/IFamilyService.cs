
public interface IFamilyService
{
    Task<FamilyDTO> CreateFamilyAsync(AddFamilyDTO familyDto);
    Task DeleteFamilyAsync(int id);
    Task<List<FamilyDTO>> GetAllFamiliesAsync();
    Task<FamilyDTO> GetByUserIdAsync(string userId);
    Task<FamilyDTO> GetFamilyByIdAsync(int id);
    Task UpdateFamilyAsync(int id, UpdateFamilyDTO familyDto);
}

