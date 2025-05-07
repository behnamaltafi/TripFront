using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class FamilyRepository : GenericRepository<Family, int>, IFamilyRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public FamilyRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }
   
}
