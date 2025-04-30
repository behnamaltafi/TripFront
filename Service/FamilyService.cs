using AutoMapper;

public class FamilyService : IFamilyService
{
    private readonly IFamilyRepository _familyRepository;
    private readonly IMapper _mapper;

    public FamilyService(IFamilyRepository familyRepository, IMapper mapper)
    {
        _familyRepository = familyRepository;
        _mapper = mapper;
    }

    public async Task<FamilyDTO> GetFamilyByIdAsync(int id)
    {
        var family = await _familyRepository.GetByIdAsync(id);
        return _mapper.Map<FamilyDTO>(family);
    }

    public async Task<List<FamilyDTO>> GetAllFamiliesAsync()
    {
        var families = await _familyRepository.GetAllAsync();
        return _mapper.Map<List<FamilyDTO>>(families);
    }

    public async Task<FamilyDTO> CreateFamilyAsync(AddFamilyDTO familyDto)
    {
        var family = _mapper.Map<Family>(familyDto);
        var createdFamily = await _familyRepository.AddAsync(family);
        return _mapper.Map<FamilyDTO>(createdFamily);
    }

    public async Task UpdateFamilyAsync(int id, UpdateFamilyDTO familyDto)
    {
        var family = await _familyRepository.GetByIdAsync(id);
        _mapper.Map(familyDto, family);
        await _familyRepository.UpdateAsync(family);
    }

    public async Task DeleteFamilyAsync(int id)
    {
        await _familyRepository.DeleteAsync(id);
    }
}