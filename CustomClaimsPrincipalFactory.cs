using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using TripFront.Data;

public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IFamilyService _familyService;

    public CustomClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        IOptions<IdentityOptions> optionsAccessor,
        IFamilyService familyService)
        : base(userManager, optionsAccessor)
    {
        _userManager = userManager;
        _familyService = familyService;
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        // Always fetch and attach familyId dynamically (but NOT save to DB)
        var family = await _familyService.GetByUserIdAsync(user.Id);
        if (family != null)
        {
            identity.AddClaim(new Claim("familyId", family.Id.ToString()));
        }

        return identity;
    }
}
