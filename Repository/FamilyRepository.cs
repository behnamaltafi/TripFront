using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TripFront.Models;

public class FamilyRepository : GenericRepository<Family, int>, IFamilyRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public FamilyRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<string> GetProfileByFamilyId(int id)
    {
        return await _context.Set<Family>().Where(x => x.Id == id).Select(x => x.ProfileImage).FirstOrDefaultAsync();
    }


}
