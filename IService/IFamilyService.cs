
using FilterPagingEfCore.Paging;

public interface IFamilyService
{
    Task<FamilyDTO> Add(AddFamilyDTO familyDto);
    Task Delete(int id);
    Task<PagingResult<FamilyDTO>> FindAllPaging(PagingParam pagingParam);
    Task<FamilyDTO> GetByUserIdAsync(string userId);
    Task<FamilyDTO> Find(int id);
    Task Update(UpdateFamilyDTO familyDto);
    string GetprofileImage();
    Task<string> GetProfileByFamilyId(int id);
}

