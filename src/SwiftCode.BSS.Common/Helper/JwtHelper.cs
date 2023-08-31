
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SwiftCode.BSS.Common.Helper;

public class JwtHelper
{
    public static string IssueJwt(TokenModelJwt tokenModel)
    {
        string iss = Appsettings.App(new string[] { "Audience", "Issuer" });
        string aud = Appsettings.App(new string[] { "Audience", "Audience" });
        string secret = Appsettings.App(new string[] { "Audience", "Secret" });

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti,tokenModel.Uid.ToString()),
            new Claim(JwtRegisteredClaimNames.Nbf,$"{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}"),
            new Claim(JwtRegisteredClaimNames.Exp,$"{DateTimeOffset.UtcNow.AddSeconds(1000).ToUnixTimeSeconds()}"),
            new Claim(ClaimTypes.Expiration,DateTimeOffset.UtcNow.AddSeconds(1000).ToString()),
            new Claim(JwtRegisteredClaimNames.Iss,iss),
            new Claim(JwtRegisteredClaimNames.Aud,aud),
        };

        claims.AddRange(tokenModel.Role.Split(',').Select(x => new Claim(ClaimTypes.Role, x)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var jwt = new JwtSecurityToken
        (
            issuer: iss,
            claims: claims,
            signingCredentials: creds
        );

        var jwtHandler = new JwtSecurityTokenHandler();
        var encodedJwt = jwtHandler.WriteToken(jwt);

        return encodedJwt;
    }

    public static TokenModelJwt SerializeJwt(string jwtStr)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        TokenModelJwt tokenModelJwt = new TokenModelJwt();

        if (!string.IsNullOrWhiteSpace(jwtStr) && jwtHandler.CanReadToken(jwtStr))
        {
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            object role;

            jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);

            tokenModelJwt = new TokenModelJwt
            {
                Uid = Convert.ToInt64(jwtToken.Id),
                Role = role == null ? "" : role.ToString()
            };

        }
        return tokenModelJwt;
    }
}
