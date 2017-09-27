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
    using ProcessingTools.Models.Contracts;

    public abstract class GenericDataServiceController<TService, TServiceModel, TRequestModel, TResponseModel, TFilter> : ApiController
        where TFilter : class, IFilter
        where TServiceModel : class, IIntegerIdentifiable
        where TService : class, IMultiDataServiceAsync<TServiceModel, TFilter>
        where TRequestModel : class
        where TResponseModel : class
    {
        private readonly TService service;

        protected GenericDataServiceController(TService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        protected abstract IMapper Mapper { get; }

        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var data = await this.service.SelectAsync(null).ConfigureAwait(false);
                if (data == null)
                {
                    return this.NotFound();
                }

                var result = data.Select(this.Mapper.Map<TResponseModel>).ToList();

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.ToString());
            }
        }

        public async Task<IHttpActionResult> GetById(int id)
        {
            try
            {
                var model = await this.service.GetByIdAsync(id).ConfigureAwait(false);
                if (model == null)
                {
                    return this.NotFound();
                }

                var result = this.Mapper.Map<TResponseModel>(model);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.ToString());
            }
        }

        public async Task<IHttpActionResult> GetPaged(string sortKey, int skip = PaginationConstants.DefaultSkip, int take = PaginationConstants.DefaultTake)
        {
            if (string.IsNullOrWhiteSpace(sortKey))
            {
                return this.BadRequest(Messages.InvalidValueForSortKeyQueryParameterMessage);
            }

            try
            {
                var data = await this.service.SelectAsync(null, skip, take, sortKey).ConfigureAwait(false);
                if (data == null)
                {
                    return this.NotFound();
                }

                var result = data.Select(this.Mapper.Map<TResponseModel>).ToList();

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Adds new entity.
        /// </summary>
        /// <param name="entities">Entity to be added.</param>
        /// <returns>OK if there is no errors; BadRequest on exception.</returns>
        public async Task<IHttpActionResult> Post([FromBody]TRequestModel[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return this.BadRequest();
            }

            try
            {
                var items = entities.Select(this.Mapper.Map<TServiceModel>).ToArray();
                var result = await this.service.InsertAsync(items).ConfigureAwait(false);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entities">Entity to be updated.</param>
        /// <returns>OK if there is no errors; BadRequest on exception.</returns>
        public async Task<IHttpActionResult> Put([FromBody]TRequestModel[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return this.BadRequest();
            }

            try
            {
                var items = entities.Select(this.Mapper.Map<TServiceModel>).ToArray();
                var result = await this.service.UpdateAsync(items).ConfigureAwait(false);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.ToString());
            }
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]TRequestModel entity)
        {
            if (entity == null)
            {
                return this.BadRequest();
            }

            try
            {
                var item = this.Mapper.Map<TServiceModel>(entity);

                var property = item.GetType().GetProperty(nameof(IIntegerIdentifiable.Id));
                property.SetValue(item, id);

                var result = await this.service.UpdateAsync(item).ConfigureAwait(false);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entities">Entity to be deleted.</param>
        /// <returns>OK if there is no errors; BadRequest on exception.</returns>
        public async Task<IHttpActionResult> Delete([FromBody]TRequestModel[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return this.BadRequest();
            }

            try
            {
                var items = entities.Select(this.Mapper.Map<TServiceModel>).ToArray();
                var result = await this.service.DeleteAsync(items).ConfigureAwait(false);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="id">Id of the entity to be deleted.</param>
        /// <returns>OK if there is no errors; BadRequest on exception.</returns>
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var result = await this.service.DeleteAsync(id).ConfigureAwait(false);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.ToString());
            }
        }

        public async Task<IHttpActionResult> Delete([FromBody]int[] ids)
        {
            try
            {
                var result = await this.service.DeleteAsync(ids.Cast<object>().ToArray()).ConfigureAwait(false);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.ToString());
            }
        }
    }
}
