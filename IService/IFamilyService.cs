
public interface IFamilyService
{
    Task<FamilyDTO> CreateFamilyAsync(AddFamilyDTO familyDto);
    Task DeleteFamilyAsync(int id);
    Task<List<FamilyDTO>> GetAllFamiliesAsync();
    Task<FamilyDTO> GetFamilyByIdAsync(int id);
    Task UpdateFamilyAsync(int id, UpdateFamilyDTO familyDto);
}

