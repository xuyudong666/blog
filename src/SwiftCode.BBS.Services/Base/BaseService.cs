using SwiftCode.BBS.IRepositories;
using SwiftCode.BBS.IRepositories.Base;
using SwiftCode.BBS.IServices.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SwiftCode.BBS.Services.Base;

public class BaseService<TEntity> :  IBaseService<TEntity> where TEntity : class
{
    private readonly IBaseRepository<TEntity> _baseRepository;
    public BaseService(IBaseRepository<TEntity> baseRepository)
    {
        _baseRepository = baseRepository;
    }
    public Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<long> GetCountAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _baseRepository.GetListAsync(predicate, cancellationToken);
    }

    public Task<List<TEntity>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> UpdateManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}
