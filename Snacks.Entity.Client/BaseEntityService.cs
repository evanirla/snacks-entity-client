using Microsoft.Extensions.DependencyInjection;
using Snacks.Entity.Client.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
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

            model = await _apiService.PostAsync<TModel>(_route.Route, model);

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

            return await _apiService.PostAsync<List<TModel>>(_route.Route, models);
        }

        public async Task DeleteOneAsync(TModel model)
        {
            await _apiService.DeleteAsync($"{_route.Route}/{model.Key}");
        }

        public async Task<TModel> GetOneAsync(object key)
        {
            return await _apiService.GetAsync<TModel>($"{_route.Route}/{key}");
        }

        public async Task<IList<TModel>> GetManyAsync(params string[] queryParams)
        {
            string routeWithQuery = _route.Route;

            if (queryParams.Length > 0)
            {
                routeWithQuery += "?";
                for (int i = 0; i < queryParams.Length; i++)
                {
                    routeWithQuery += queryParams[i];

                    if (i < queryParams.Length - 1)
                    {
                        routeWithQuery += "&";
                    }
                }   
            }

            return await _apiService.GetAsync<List<TModel>>(routeWithQuery);
        }

        public async Task UpdateOneAsync(TModel model)
        {
            await _apiService.PostAsync($"{_route.Route}/{model.Key}", model);
        }

        async Task<IEntityModel> IEntityService.GetOneAsync(object key)
        {
            return await GetOneAsync(key);
        }

        async Task<IList<IEntityModel>> IEntityService.GetManyAsync(params string[] queryParams)
        {
            return (await GetManyAsync(queryParams)).Select(x => (IEntityModel)x).ToList();
        }

        public async Task<IEntityModel> CreateOneAsync(IEntityModel model)
        {
            return await CreateOneAsync((TModel)model);
        }

        public async Task<IList<IEntityModel>> CreateManyAsync(IList<IEntityModel> models)
        {
            return (await CreateManyAsync((IList<TModel>)models)).Select(x => (IEntityModel)x).ToList();
        }

        public async Task UpdateOneAsync(IEntityModel model)
        {
            await UpdateOneAsync((TModel)model);
        }

        public async Task DeleteOneAsync(IEntityModel model)
        {
            await DeleteOneAsync((TModel)model);
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
