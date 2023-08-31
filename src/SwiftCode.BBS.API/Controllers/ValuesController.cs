using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SwiftCode.BBS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = "SystemOrAdmin")]
        public IEnumerable<string> Get()
        {
            return new string[] { "真不错" };
        }
    }
}
