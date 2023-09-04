﻿using SwiftCode.BBS.IServices.Base;
using SwiftCode.BBS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SwiftCode.BBS.IServices;

public interface IArticleServices : IBaseServices<Article>
{
    Task<Article> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<Article> GetArticleDetailsAsync(int id, CancellationToken cancellationToken = default);

    Task AddArticleCollection(int id, int userId, CancellationToken cancellationToken = default);

    Task AddArticleComments(int id, int userId, string content, CancellationToken cancellationToken = default);
    Task AdditionalItemAsync(Article entity, bool v, int n = 0);
}