using SwiftCode.BBS.IRepositories;
using SwiftCode.BBS.Model.Models;
using SwiftCode.BBS.Repositories.EfContext;
using System.Linq.Expressions;

namespace SwiftCode.BBS.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly SwiftCodeBbsContext _context;
    public ArticleRepository()
    {
        _context = new SwiftCodeBbsContext();
    }
    public void Add(Article model)
    {
        _context.Articles.Add(model);
        _context.SaveChanges();
    }

    public void Delete(Article model)
    {
        _context.Articles.Remove(model);
        _context.SaveChanges();
    }

    public List<Article> GetAll(Expression<Func<Article, bool>> whereExpression)
    {
        return _context.Articles.Where(whereExpression).ToList();
    }

    public void Update(Article model)
    {
        _context.Articles.Remove(model);
        _context.SaveChanges();
    }
}
