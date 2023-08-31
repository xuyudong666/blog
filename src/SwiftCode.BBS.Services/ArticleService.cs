using SwiftCode.BBS.IRepositories;
using SwiftCode.BBS.IServices;
using SwiftCode.BBS.Model.Models;
using SwiftCode.BBS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SwiftCode.BBS.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository = new ArticleRepository();

    public void Add(Article model)
    {
        _articleRepository.Add(model);  
    }

    public void Delete(Article model)
    {
        _articleRepository.Delete(model);
    }

    public List<Article> GetAll(Expression<Func<Article, bool>> whereExpression)
    {
        return _articleRepository.GetAll(whereExpression);
    }

    public void Update(Article model)
    {
        _articleRepository.Update(model);
    }
}
