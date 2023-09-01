using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftCode.BBS.IServices;
using SwiftCode.BBS.Model.Models;

namespace SwiftCode.BBS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<List<Article>> Get(int id)
        {
            return await _articleService.GetListAsync(d => d.Id == id);
        }
    }
}
