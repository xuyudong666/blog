using SwiftCode.BBS.IRepositories;
using SwiftCode.BBS.IServices;
using SwiftCode.BBS.Model.Models;
using SwiftCode.BBS.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SwiftCode.BBS.Services;

public class ArticleService : BaseService<Article>, IArticleService
{
    public ArticleService(IArticleRepository articleRepository) 
        : base(articleRepository)
    {
        
    }
}
