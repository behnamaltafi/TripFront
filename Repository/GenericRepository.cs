
using AutoMapper;
using FilterPagingEfCore.Extenstion;
using FilterPagingEfCore.Paging;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using BusinessExceptionStructure;

public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId>, new()
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapperService;
    private readonly bool _entityHasUpdateLogAttribute;
    private readonly bool _entityHasRowVersion;

    public GenericRepository(AppDbContext dbContext, IMapper mapperService)
    {
        _dbContext = dbContext;
        _mapperService = mapperService;

        _entityHasRowVersion = EntityHasRowVersion();
    }

    #region Add
    public async Task Add(TEntity model)
    {
        await _dbContext.AddAsync(model);
    }

    public async Task Add<DTO>(DTO model)
    {
        await Add(_mapperService.Map<TEntity>(model));
    }

    public async Task AddRange(List<TEntity> model)
    {
        await _dbContext.AddRangeAsync(model);
    }

    public async Task AddRange<DTO>(List<DTO> model)
    {
        await AddRange(_mapperService.Map<List<TEntity>>(model));
    }
    #endregion

    #region Update
    public async Task Update(TEntity model)
    {
        if (_entityHasUpdateLogAttribute)
        {
            //چون میخوایم تغییرات موجودیت را لاگ کنیم، بنابراین به مقادیر قبل از تغییر موجودیت نیز نیاز داریم
            //بنابراین یک بار دیگر موجودیت را به حالت
            //AsTracking
            //از دیتابیس میخونیم
            var entity = await FindAsTracking(model.Id);

            if (entity == null)
            {
                throw new BusinessException();
            }

            //چون در حالت
            //AsTracking
            //هستیم، تغییر در موجودیت به صورت اتوماتیک شناسایی میشود و بنابراین نیازی به نوشتن دستور
            //Update
            //نمیباشد
            _mapperService.Map(model, entity);

            UpdateRowVersionInChangeTracker(model);
        }
        else
        {
            _dbContext.Update(model);
        }
    }

    public async Task Update<DTO>(DTO model)
    {
        var id = GetIdFromObject(model);

        var entity = await FindAsTracking(id);

        if (entity == null)
        {
            throw new BusinessException(Messages.RecordNotFound);
        }

        //چون در حالت
        //AsTracking
        //هستیم، تغییر در موجودیت به صورت اتوماتیک شناسایی میشود و بنابراین نیازی به نوشتن دستور
        //Update
        //نمیباشد
        _mapperService.Map(model, entity);

        UpdateRowVersionInChangeTracker(model);
    }

    public async Task UpdateRange(List<TEntity> models)
    {
        if (_entityHasUpdateLogAttribute)
        {
            var entities = await _dbContext.Set<TEntity>().AsTracking().Where(e => models.Select(i => i.Id).ToList().Distinct().Contains(e.Id)).ToListAsync();
            models.ForEach(model =>
            {
                var entity = entities.Find(i => i.Id.Equals(model.Id));

                _mapperService.Map(model, entity);

                UpdateRowVersionInChangeTracker(model);
            });
        }
        else
        {
            _dbContext.UpdateRange(models);
        }
    }
    #endregion

    #region AddOrUpdate
    public async Task AddOrUpdate<DTO>(DTO model)
    {
        var id = GetIdFromObject(model);
        var entity = await Find(id);
        if (entity == null)
        {
            await Add(model);
        }
        else
        {
            await Update(_mapperService.Map(model, entity));
        }
    }
    #endregion

    #region Remove
    public async Task Remove(TId id)
    {
        //اگر موجودیت
        //RowVersion
        //داشته باشد، اینجا اعتبار سنجی نمیشود

        var entity = new TEntity
        {
            Id = id
        };

        //var entity = await Find(id);
        //if (entity == null)
        //{
        //    throw new BusinessException(Messages.RecordNotFound);
        //}
        await Remove(entity);
    }

    public async Task Remove(TEntity model)
    {
        _dbContext.Remove(model);
    }

    public async Task Remove<DTO>(DTO model)
    {
        await Remove(_mapperService.Map<TEntity>(model));
    }

    public async Task RemoveRange(List<TEntity> model)
    {
        _dbContext.RemoveRange(model);
    }

    public async Task RemoveRange(Expression<Func<TEntity, bool>> condition)
    {
        var items = await FindAll(condition);
        _dbContext.RemoveRange(items);
    }
    #endregion

    #region Soft Remove
    public async Task SoftRemove(TId id)
    {
        //اگر موجودیت
        //RowVersion
        //داشته باشد، اینجا اعتبار سنجی نمیشود

        var entity = await Find(id);
        if (entity == null)
        {
            throw new BusinessException(Messages.RecordNotFound);
        }
        await SoftRemove(entity);
    }

    public async Task SoftRemove(TEntity model)
    {
        //اگر قبل از اینکه وارد این تابع بشود
        //تغییری در 
        //model
        //ایجاد شده باشد، در اینجا جدا از اینکه فیلد 
        //IsRemoved=true
        //میشود، بقیه تغییرات نیز روی موجودیت اعمال میشود            
        SetIsRemovedPropertyToTrue(model);
        await Update(model);
    }

    public async Task SoftRemove<DTO>(DTO model)
    {
        var id = GetIdFromObject(model);

        var entity = await FindAsTracking(id);

        if (entity == null)
        {
            throw new BusinessException(Messages.RecordNotFound);
        }

        //چون در حالت
        //AsTracking
        //هستیم، تغییر در موجودیت به صورت اتوماتیک شناسایی میشود و بنابراین نیازی به نوشتن دستور
        //Update
        //نمیباشد
        SetIsRemovedPropertyToTrue(entity);

        UpdateRowVersionInChangeTracker(model);
    }

    public async Task SoftRemoveRange(List<TEntity> model)
    {
        model.ForEach(e =>
        {
            SetIsRemovedPropertyToTrue(e);
        });
        await UpdateRange(model);
    }

    public async Task SoftRemoveRange(Expression<Func<TEntity, bool>> condition)
    {
        var items = await FindAll(condition);
        items.ForEach(e =>
        {
            SetIsRemovedPropertyToTrue(e);
        });
        await UpdateRange(items);
    }
    #endregion

    #region Any
    public async Task<bool> Any(Expression<Func<TEntity, bool>> condition)
    {
        var result = await _dbContext.Set<TEntity>().AnyAsync(condition);
        return result;
    }
    #endregion

    #region Count
    public async Task<int> Count(Expression<Func<TEntity, bool>> condition)
    {
        var result = await _dbContext.Set<TEntity>().Where(condition).CountAsync();
        return result;
    }
    #endregion

    #region Find
    public async Task<TEntity> Find(TId id)
    {
        var result = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id.Equals(id));
        return result;
    }

    public async Task<TEntity> Find(TId id, params Expression<Func<TEntity, object>>[] navigationProperties)
    {
        var iqueryable = _dbContext.Set<TEntity>().Where(e => e.Id.Equals(id));
        foreach (var item in navigationProperties)
        {
            iqueryable = iqueryable.Include(item);
        }
        var result = await iqueryable.FirstOrDefaultAsync();
        return result;
    }

    public async Task<TEntity> Find(TId id, params string[] navigationProperties)
    {
        var iqueryable = _dbContext.Set<TEntity>().Where(e => e.Id.Equals(id));
        foreach (var item in navigationProperties)
        {
            iqueryable = iqueryable.Include(item);
        }
        var result = await iqueryable.FirstOrDefaultAsync();
        return result;
    }

    public async Task<TEntity> Find(Expression<Func<TEntity, bool>> condition)
    {
        var result = await _dbContext.Set<TEntity>().Where(condition).FirstOrDefaultAsync();
        return result;
    }

    public async Task<TEntity> Find(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] navigationProperties)
    {
        var iqueryable = _dbContext.Set<TEntity>().Where(condition);
        foreach (var item in navigationProperties)
        {
            iqueryable = iqueryable.Include(item);
        }
        var result = await iqueryable.FirstOrDefaultAsync();
        return result;
    }

    public async Task<TEntity> Find(Expression<Func<TEntity, bool>> condition, params string[] navigationProperties)
    {
        var iqueryable = _dbContext.Set<TEntity>().Where(condition);
        foreach (var item in navigationProperties)
        {
            iqueryable = iqueryable.Include(item);
        }
        var result = await iqueryable.FirstOrDefaultAsync();
        return result;
    }

    public async Task<DTO> Find<DTO>(TId id)
    {
        var result = await _mapperService.ProjectTo<DTO>(_dbContext.Set<TEntity>().Where(e => e.Id.Equals(id))).FirstOrDefaultAsync();
        return result;
    }

    public async Task<DTO> Find<DTO>(Expression<Func<TEntity, bool>> condition)
    {
        var result = await _mapperService.ProjectTo<DTO>(_dbContext.Set<TEntity>().Where(condition)).FirstOrDefaultAsync();
        return result;
    }

    public async Task<TId> FindId(Expression<Func<TEntity, bool>> condition)
    {
        var result = await _dbContext.Set<TEntity>().Where(condition).Select(e => e.Id).FirstOrDefaultAsync();
        return result;
    }
    #endregion

    #region FindAll
    public async Task<List<TEntity>> FindAll()
    {
        var result = await _dbContext.Set<TEntity>().ToListAsync();
        return result;
    }

    public async Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> condition)
    {
        var result = await _dbContext.Set<TEntity>().Where(condition).ToListAsync();
        return result;
    }

    public async Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] navigationProperties)
    {
        var iqueryable = _dbContext.Set<TEntity>().Where(condition).AsQueryable();
        foreach (var item in navigationProperties)
        {
            iqueryable = iqueryable.Include(item);
        }
        var result = await iqueryable.ToListAsync();
        return result;
    }

    public async Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> condition, params string[] navigationProperties)
    {
        var iqueryable = _dbContext.Set<TEntity>().Where(condition).AsQueryable();
        foreach (var item in navigationProperties)
        {
            iqueryable = iqueryable.Include(item);
        }
        var result = await iqueryable.ToListAsync();
        return result;
    }

    public async Task<List<TEntity>> FindAll(params Expression<Func<TEntity, object>>[] navigationProperties)
    {
        var iqueryable = _dbContext.Set<TEntity>().AsQueryable();
        foreach (var item in navigationProperties)
        {
            iqueryable = iqueryable.Include(item);
        }
        var result = await iqueryable.ToListAsync();
        return result;
    }

    public async Task<List<TEntity>> FindAll(params string[] navigationProperties)
    {
        var iqueryable = _dbContext.Set<TEntity>().AsQueryable();
        foreach (var item in navigationProperties)
        {
            iqueryable = iqueryable.Include(item);
        }
        var result = await iqueryable.ToListAsync();
        return result;
    }

    public async Task<List<DTO>> FindAll<DTO>()
    {
        var result = await _mapperService.ProjectTo<DTO>(_dbContext.Set<TEntity>()).ToListAsync();
        return result;
    }

    public async Task<List<DTO>> FindAll<DTO>(Expression<Func<TEntity, bool>> condition)
    {
        var result = await _mapperService.ProjectTo<DTO>(_dbContext.Set<TEntity>().Where(condition)).ToListAsync();
        return result;
    }

    public async Task<PagingResult<DTO>> FindAllPaging<DTO>(PagingParam filter) where DTO : class
    {
        var result = await _mapperService.ProjectTo<DTO>(_dbContext.Set<TEntity>()).FilterPaging(filter);
        return result;
    }

    public async Task<PagingResult<DTO>> FindAllPaging<DTO>(PagingParam filter, Expression<Func<TEntity, bool>> condition) where DTO : class
    {
        var result = await _mapperService.ProjectTo<DTO>(_dbContext.Set<TEntity>().Where(condition)).FilterPaging(filter);
        return result;
    }
    #endregion

    #region Save
    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
        ChangeStateUnchangedToDetached();
    }
    #endregion

    #region PrivateMethods   
    private async Task<TEntity> FindAsTracking(TId id)
    {
        var result = await _dbContext.Set<TEntity>().AsTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));
        return result;
    }

    private TId GetIdFromObject(object model)
    {
        var result = model.GetType().GetProperties().Where(e => e.Name.ToLower() == "id").First().GetValue(model);
        return (TId)result;
    }

    private void SetIsRemovedPropertyToTrue(TEntity entity)
    {
        _dbContext.Entry(entity).Property("IsRemoved").CurrentValue = true;
    }

    private void UpdateRowVersionInChangeTracker(object model)
    {
        //اگر موجودیت ورودی، فیلد
        //RowVersion
        //داشته باشد و اگر همان موجودیت در اینجا به صورت 
        //AsTracking
        //از دیتابیس فراخوانی بشود، هنگام ذخیره به جای اینکه فیلد
        //RowVersion
        //موجودیت ورودی معیار قرار بگیرد، فیلد 
        //RowVersion
        //موجودیت مجددا فراخوانی شده معیار قرار میگیرد
        //که این اشتباه هست و عملکرد فیلد
        //RowVersion
        //را خراب میکند، کد زیر مشکل را برطرف میکند                      
        if (_entityHasRowVersion)
        {
            var id = model.GetType().GetProperties().Where(e => e.Name.ToLower() == "id").First().GetValue(model);
            var entityChangeTracker = _dbContext.ChangeTracker.Entries<TEntity>().First(e => e.Entity.Id.Equals(id));

            var inputRowVersion = model.GetType().GetProperties().FirstOrDefault(e => e.Name == "RowVersion");
            entityChangeTracker.Properties.First(e => e.Metadata.Name == "RowVersion").OriginalValue = inputRowVersion?.GetValue(model);
        }
    }

    private bool EntityHasRowVersion()
    {
        var result = typeof(TEntity).GetProperties().Any(e => e.Name == "RowVersion");
        return result;
    }

    private void ChangeStateUnchangedToDetached()
    {
        _dbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Unchanged).ToList().ForEach(e => e.State = EntityState.Detached);
    }
    #endregion
}

