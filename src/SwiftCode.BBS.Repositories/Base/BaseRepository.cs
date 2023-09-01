using Microsoft.EntityFrameworkCore;
using SwiftCode.BBS.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using SwiftCode.BBS.EntityFramework.EfContext;

namespace SwiftCode.BBS.Repositories.Base;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly SwiftCodeBbsContext _context;
    public BaseRepository(SwiftCodeBbsContext context)
    {
        _context = context;
    }

    protected SwiftCodeBbsContext DbContext()
    {
        return _context;
    }

    public async Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity>().Remove(entity);
        if(autoSave)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        var dbSet = _context.Set<TEntity>();

        var entities = await dbSet.Where(predicate).ToListAsync(cancellationToken);

        await DeleteManyAsync(entities,autoSave,cancellationToken);

        if(autoSave)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        _context.RemoveRange(entities);
        if (autoSave)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().Where(predicate).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var entity = await FindAsync(predicate, cancellationToken);
        return entity ?? throw new NullReferenceException($"{nameof(predicate)}数据不存在");
    }

    public Task<long> GetCountAsync(CancellationToken cancellationToken = default)
    {
        return _context.Set<TEntity>().LongCountAsync(cancellationToken);
    }

    public Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _context.Set<TEntity>().Where(predicate).LongCountAsync(cancellationToken);
    }

    public Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return _context.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _context.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);
    }

    public Task<List<TEntity>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, CancellationToken cancellationToken = default)
    {
        return _context.Set<TEntity>().OrderBy(sorting).Skip(skipCount).Take(maxResultCount).ToListAsync(cancellationToken);
    }

    public async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken token = default)
    {
        var savedEntity = (await _context.Set<TEntity>().AddAsync(entity, token)).Entity;
        if (autoSave)
        {
            await _context.SaveChangesAsync(token);
        }
        return savedEntity;
    }

    public async Task InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken token = default)
    {
        var entityArray = entities.ToArray();
        await _context.Set<TEntity>().AddRangeAsync(entityArray, token);
        if (autoSave)
        {
            await _context.SaveChangesAsync(token);
        }
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        _context.Attach(entity);
        var updatedEntity = _context.Update(entity).Entity;
        if(autoSave)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        return updatedEntity;
    }

    public async Task UpdateManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken token = default)
    {
        _context.Set<TEntity>().UpdateRange(entities);
        if(autoSave)
        {
            await _context.SaveChangesAsync(token);
        }
    }
}
