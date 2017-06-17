namespace ProcessingTools.Web.Api.Abstractions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using AutoMapper;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Contracts.Services.Data;

    public class GenericDataServiceController<TService, TServiceModel, TRequestModel, TResponseModel, TFilter> : ApiController
        where TFilter : class, IFilter
        where TServiceModel : class
        where TService : class, IMultiDataServiceAsync<TServiceModel, TFilter>
        where TRequestModel : class
        where TResponseModel : class
    {
        private readonly TService service;
        private readonly IMapper mapper;

        public GenericDataServiceController(TService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<TServiceModel, TResponseModel>();
                c.CreateMap<TRequestModel, TServiceModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        public async Task<IHttpActionResult> GetAll()
        {
            var result = (await this.service.SelectAsync(null))
                .Select(this.mapper.Map<TResponseModel>)
                .ToList();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        public async Task<IHttpActionResult> GetById(string id)
        {
            if (!int.TryParse(id, out int parsedId))
            {
                return this.BadRequest("Invalid id.");
            }

            var result = this.mapper.Map<TResponseModel>(await this.service.GetByIdAsync(parsedId));

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        public async Task<IHttpActionResult> GetPaged(string sortKey, string skip, string take = PagingConstants.DefaultTakeString)
        {
            if (string.IsNullOrWhiteSpace(sortKey))
            {
                return this.BadRequest(Messages.InvalidValueForSortKeyQueryParameterMessage);
            }

            if (!int.TryParse(skip, out int skipItemsCount))
            {
                return this.BadRequest(Messages.InvalidValueForSkipQueryParameterMessage);
            }

            if (!int.TryParse(take, out int takeItemsCount))
            {
                return this.BadRequest(Messages.InvalidValueForTakeQueryParameterMessage);
            }

            try
            {
                var result = (await this.service.SelectAsync(null, skipItemsCount, takeItemsCount, sortKey))
                    .Select(this.mapper.Map<TResponseModel>)
                    .ToList();

                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch
            {
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Adds new entity.
        /// </summary>
        /// <param name="entity">Entity to be added.</param>
        /// <returns>Ok if there is no errors; BadRequest on exception.</returns>
        public async Task<IHttpActionResult> Post(TRequestModel entity)
        {
            var item = this.mapper.Map<TServiceModel>(entity);
            try
            {
                await this.service.InsertAsync(item);
            }
            catch
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        /// <returns>Ok if there is no errors; BadRequest on exception.</returns>
        public async Task<IHttpActionResult> Put(TRequestModel entity)
        {
            var item = this.mapper.Map<TServiceModel>(entity);
            try
            {
                await this.service.UpdateAsync(item);
            }
            catch
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        /// <returns>Ok if there is no errors; BadRequest on exception.</returns>
        public async Task<IHttpActionResult> Delete(TRequestModel entity)
        {
            var item = this.mapper.Map<TServiceModel>(entity);
            try
            {
                await this.service.DeleteAsync(item);
            }
            catch
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="id">Id of the entity to be deleted.</param>
        /// <returns>Ok if there is no errors; BadRequest on exception.</returns>
        public async Task<IHttpActionResult> Delete(string id)
        {
            if (!int.TryParse(id, out int parsedId))
            {
                return this.BadRequest(id);
            }

            try
            {
                await this.service.DeleteAsync(id);
            }
            catch
            {
                return this.BadRequest();
            }

            return this.Ok();
        }
    }
}
