using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftCode.BBS.IServices;
using SwiftCode.BBS.Services;

namespace SwiftCode.BBS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatController : ControllerBase
    {
        [HttpGet]
        public int Get(int i,int j)
        {
            ICalculatService calculatService = new CalculatService();
            return calculatService.Sum(i, j);
        }
    }
}
