using Microsoft.AspNetCore.Mvc;
using SwiftCode.BSS.Common.Helper;

namespace SwiftCode.BBS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpGet]
    public async Task<object> GetJwtStr(string name, string pass)
    {
        TokenModelJwt tokenModel = new() { Uid = 1, Role = "Admin" };
        var jwtStr = JwtHelper.IssueJwt(tokenModel);
        var suc = true;
        return Ok(new
        {
            sucess = suc,
            token = jwtStr
        });
    }

}
