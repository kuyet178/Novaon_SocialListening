using System.Security.Claims;
using AioCore.Domain.Aggregates.IdentityAggregate;
using AioCore.Domain.Common.Constants;
using AioCore.Domain.DbContexts;
using AioCore.Shared.Extensions;
using AioCore.Shared.ValueObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AioCore.Services.Providers;

public class ClaimsTransformation : IClaimsTransformation
{
    private readonly UserManager<User> _userManager;
    private readonly IdentityContext _settingsContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClaimsTransformation(
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor,
        IdentityContext settingsContext)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _settingsContext = settingsContext;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identity = principal.Identities.FirstOrDefault(c => c.IsAuthenticated);
        if (identity == null) return principal;

        var user = await _userManager.GetUserAsync(principal);
        var host = _httpContextAccessor.HttpContext?.Request.Headers[RequestHeaders.Host].ToString();
        if (user == null) return principal;
        var roles = await _userManager.GetRolesAsync(user);
        identity.AddClaim(new Claim(nameof(UserClaimsValue.Id), user.Id.ToString()));
        identity.AddClaim(new Claim(nameof(UserClaimsValue.UserName), user.Id.ToString()));
        identity.AddClaim(new Claim(nameof(UserClaimsValue.Email), user.Email));
        identity.AddClaim(new Claim(nameof(UserClaimsValue.Host), host ?? string.Empty));
        identity.AddClaim(new Claim(nameof(UserClaimsValue.Roles), roles.ToJson()));

        if (!principal.HasClaim(c => c.Type == ClaimTypes.GivenName))
        {
            identity.AddClaim(new Claim(nameof(UserClaimsValue.FullName), user.FullName));
        }

        return new ClaimsPrincipal(identity);
    }
}