using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Snacks.Entity.Client
{
    public interface IEntityService<TModel> where TModel : IEntityModel
    {
        Task<TModel> GetOneAsync(object key);
        Task<IList<TModel>> GetManyAsync(params string[] queryParams);
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
