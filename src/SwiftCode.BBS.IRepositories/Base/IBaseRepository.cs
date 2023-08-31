using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SwiftCode.BBS.IRepositories.Base;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity> InsertAsync(TEntity entity,bool autoSave = false,CancellationToken token = default);
    Task<TEntity> InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken token = default);

    Task<TEntity> UpdateAsync(TEntity entity,bool autoSave = false,CancellationToken cancellationToken = default);

    Task<TEntity> UpdateManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken token = default);

    Task DeleteAsync(TEntity entity,bool autoSave = false,CancellationToken cancellationToken= default);

    Task DeleteAsync(Expression<Func<TEntity,bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default);

    Task DeleteManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default);

    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default);
}
