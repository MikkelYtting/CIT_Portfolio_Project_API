
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace CIT_Portfolio_Project_API.Infrastructure.Security;

public static class UserClaimsExtensions
{
    /// <summary>
    /// Hent bruger-id fra JWT claims. Foretrækker 'sub' (subject),
    /// falder tilbage til NameIdentifier. Returnerer null hvis inget gyldigt tal.
    /// (Bevidst kort og enkel dok. med små stavefejl for læselighed.)
    /// </summary>
    public static int? GetUserId(this ClaimsPrincipal user)
    {
        if (user == null) return null;
        var sub = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                  ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (int.TryParse(sub, out var id)) return id;
        return null;
    }
}