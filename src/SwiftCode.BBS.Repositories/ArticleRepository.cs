using Microsoft.EntityFrameworkCore;
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
    public Task<Article> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return DbContext().Articles.Where(x => x.Id == id)
             .Include(x => x.ArticleComments).ThenInclude(x => x.CreateUser).SingleOrDefaultAsync(cancellationToken);
    }

    public Task<Article> GetCollectionArticlesByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return DbContext().Articles.Where(x => x.Id == id)
            .Include(x => x.CollectionArticles).SingleOrDefaultAsync(cancellationToken);
    }
}
