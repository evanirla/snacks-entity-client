using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Snacks.Entity.Client
{
    public interface IEntityService
    {
        Task<IEntityModel> GetOneAsync(object key);
        Task<IList<IEntityModel>> GetManyAsync(params string[] queryParams);
        Task<IEntityModel> CreateOneAsync(IEntityModel model);
        Task<IList<IEntityModel>> CreateManyAsync(IList<IEntityModel> models);
        Task UpdateOneAsync(IEntityModel model);
        Task DeleteOneAsync(IEntityModel model);
    }

    public interface IEntityService<TModel> : IEntityService where TModel : IEntityModel
    {
        new Task<TModel> GetOneAsync(object key);
        new Task<IList<TModel>> GetManyAsync(params string[] queryParams);
        Task<TModel> CreateOneAsync(TModel model);
        Task<IList<TModel>> CreateManyAsync(IList<TModel> models);
        Task UpdateOneAsync(TModel model);
        Task DeleteOneAsync(TModel model);
    }

    public interface IEntityService<TModel, TKey>
        where TModel : IEntityModel<TKey>
    {
        Task<TModel> GetOneAsync(TKey key);
    }
}
