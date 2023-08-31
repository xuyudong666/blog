using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftCode.BBS.IServices;
using SwiftCode.BBS.Model.Models;
using SwiftCode.BBS.Services;

namespace SwiftCode.BBS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {

        [HttpGet("{id}",Name ="Get")]
        public List<Article> Get(int id)
        {
            IArticleService articleService = new ArticleService();
            return articleService.GetAll(d=>d.Id == id);
        }
    }
}
