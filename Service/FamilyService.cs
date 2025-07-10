using AutoMapper;
using FilterPagingEfCore.Extenstion;
using FilterPagingEfCore.Paging;

public class FamilyService : IFamilyService
{
    private readonly IFamilyRepository _familyRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public FamilyService(IFamilyRepository familyRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _familyRepository = familyRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }
    public int GetFamilyId()
    {
        var familyId = _httpContextAccessor.HttpContext?.User.FindFirst("familyId")?.Value;
        if (familyId == null)
            throw new UnauthorizedAccessException();
        return int.Parse(familyId);
    }
    public  string GetprofileImage()
    {
        var profileImage = _httpContextAccessor.HttpContext?.User.FindFirst("profileImage")?.Value;
        if (profileImage == null)
            throw new UnauthorizedAccessException();
        return profileImage;
    }
    public async Task<FamilyDTO> Find(int id)
    {
        var family = await _familyRepository.Find<FamilyDTO>(id);
        return family;
    }
    public async Task<string> GetProfileByFamilyId(int id)
    {
        var profileImage = await _familyRepository.GetProfileByFamilyId(id);
        return profileImage;
    }
    public async Task<FamilyDTO> GetByUserIdAsync(string userId)
    {
        var family = await _familyRepository.Find<FamilyDTO>(x => x.UserId == userId);
        return family;
    }

    public async Task<PagingResult<FamilyDTO>> FindAllPaging(PagingParam pagingParam)
    {
        var families = await _familyRepository.FindAllPaging<FamilyDTO>(pagingParam);
        return families;
    }

    public async Task<FamilyDTO> Add(AddFamilyDTO familyDto)
    {
        await _familyRepository.Add(familyDto);
        await _familyRepository.Save();
        return _mapper.Map<FamilyDTO>(familyDto);
    }

    public async Task Update(UpdateFamilyDTO familyDto)
    {
        await _familyRepository.Update(familyDto);
        await _familyRepository.Save();

    }

    public async Task Delete(int id)
    {
        await _familyRepository.Remove(id);
        await _familyRepository.Save();

    }
}