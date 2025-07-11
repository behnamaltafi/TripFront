
using TripFront.Models;

public interface IFamilyRepository : IGenericRepository<Family, int>
{
    Task<string> GetProfileByFamilyId(int id);
}

