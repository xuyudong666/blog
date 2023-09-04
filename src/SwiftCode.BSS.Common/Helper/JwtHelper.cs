﻿
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace SwiftCode.BSS.Common.Helper;

public class JwtHelper
{

    /// <summary>
    /// 颁发JWT字符串
    /// </summary>
    /// <param name="tokenModel"></param>
    /// <returns></returns>
    public static string IssueJwt(TokenModelJwt tokenModel)
    {
        string iss = Appsettings.App(new string[] { "Audience", "Issuer" });
        string aud = Appsettings.App(new string[] { "Audience", "Audience" });
        string secret = Appsettings.App(new string[] { "Audience", "Secret" });

        var claims = new List<Claim>
                {
                new Claim(JwtRegisteredClaimNames.Jti, tokenModel.Uid.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                //这个就是过期时间，目前是过期1000秒，可自定义，注意JWT有自己的缓冲过期时间
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(100000)).ToUnixTimeSeconds()}"),
                new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(1000).ToString()),
                new Claim(JwtRegisteredClaimNames.Iss,iss),
                new Claim(JwtRegisteredClaimNames.Aud,aud),

               };

        // 可以将一个用户的多个角色全部赋予；
        claims.AddRange(tokenModel.Role.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

        //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            issuer: iss,
            claims: claims,
            signingCredentials: creds);

        var jwtHandler = new JwtSecurityTokenHandler();
        var encodedJwt = jwtHandler.WriteToken(jwt);

        return encodedJwt;
    }

    /// <summary>
    /// 解析
    /// </summary>
    /// <param name="jwtStr"></param>
    /// <returns></returns>
    public static TokenModelJwt SerializeJwt(string jwtStr)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        TokenModelJwt tokenModelJwt = new TokenModelJwt();

        // token校验
        if (!string.IsNullOrEmpty(jwtStr) && jwtHandler.CanReadToken(jwtStr))
        {

            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);

            object role;

            jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);

            tokenModelJwt = new TokenModelJwt
            {
                Uid = Convert.ToInt32(jwtToken.Id),
                Role = role == null ? "" : role.ToString()
            };
        }
        return tokenModelJwt;
    }

    /// <summary>
    /// 授权解析jwt
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public static TokenModelJwt ParsingJwtToken(HttpContext httpContext)
    {
        if (!httpContext.Request.Headers.ContainsKey("Authorization"))
            return null;
        var tokenHeader = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        TokenModelJwt tm = SerializeJwt(tokenHeader);
        return tm;
    }

}
