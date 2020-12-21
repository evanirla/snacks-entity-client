using Microsoft.Extensions.DependencyInjection;
using Snacks.Entity.Client.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Snacks.Entity.Client
{
    public class BaseEntityService<TModel> : IEntityService<TModel>
        where TModel : IEntityModel
    {
        protected readonly IApiService _apiService;
        protected readonly RouteAttribute _route;

        public BaseEntityService(IServiceProvider serviceProvider)
        {
            _apiService = serviceProvider.GetRequiredService<IApiService>();

            if (GetType().IsDefined(typeof(RouteAttribute)))
            {
                _route = GetType().GetCustomAttribute<RouteAttribute>();
            }
            else
            {
                // TODO: Throw exception?
            }
        }

        public async Task<TModel> CreateOneAsync(TModel model)
        {
            if (model.IdempotencyKey == null)
            {
                model.IdempotencyKey = Guid.NewGuid().ToString();
            }

            model = await _apiService.PostAsync<TModel>(_route.Uri, model);

            return model;
        }

        public async Task<IList<TModel>> CreateManyAsync(IList<TModel> models)
        {
            foreach (TModel model in models)
            {
                if (model.IdempotencyKey == null)
                {
                    model.IdempotencyKey = Guid.NewGuid().ToString();
                }
            }

            return await _apiService.PostAsync<List<TModel>>(_route.Uri, models);
        }

        public async Task DeleteOneAsync(TModel model)
        {
            await _apiService.DeleteAsync($"{_route.Uri}/{model.Key}");
        }

        public async Task<TModel> GetOneAsync(object key)
        {
            return await _apiService.GetAsync<TModel>($"{_route.Uri}/{key}");
        }

        public async Task<IList<TModel>> GetManyAsync(params string[] queryParams)
        {
            return await _apiService.GetAsync<List<TModel>>(_route.Uri);
        }

        public async Task UpdateOneAsync(TModel model)
        {
            await _apiService.PostAsync($"{_route.Uri}/{model.Key}", model);
        }
    }

    public class BaseEntityService<TModel, TKey> : BaseEntityService<TModel>, IEntityService<TModel, TKey>
        where TModel : IEntityModel<TKey>
    {
        public BaseEntityService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            
        }

        public async Task<TModel> GetOneAsync(TKey key)
        {
            return await GetOneAsync((object)key);
        }
    }
}
