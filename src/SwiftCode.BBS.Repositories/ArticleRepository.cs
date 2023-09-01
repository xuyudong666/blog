using SwiftCode.BBS.EntityFramework.EfContext;
using SwiftCode.BBS.IRepositories;
using SwiftCode.BBS.Model.Models;
using SwiftCode.BBS.Repositories.Base;

namespace SwiftCode.BBS.Repositories;

public class ArticleRepository : BaseRepository<Article>, IArticleRepository
{
    public ArticleRepository(SwiftCodeBbsContext context) : base(context)
    {
    }
}
