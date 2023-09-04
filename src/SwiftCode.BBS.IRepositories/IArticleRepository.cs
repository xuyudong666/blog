using SwiftCode.BBS.IRepositories.Base;
using SwiftCode.BBS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SwiftCode.BBS.IRepositories;

public interface IArticleRepository : IBaseRepository<Article>
{
    Task<Article> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<Article> GetCollectionArticlesByIdAsync(int id, CancellationToken cancellationToken = default);
}
