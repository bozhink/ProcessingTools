namespace ProcessingTools.Web.Api.Abstractions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using AutoMapper;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Data;

    public abstract class GenericDataServiceController<TService, TServiceModel, TRequestModel, TResponseModel, TFilter> : ApiController
        where TFilter : class, IFilter
        where TServiceModel : class, IIntegerIdentifiable
        where TService : class, IMultiDataServiceAsync<TServiceModel, TFilter>
        where TRequestModel : class
        where TResponseModel : class
    {
        private readonly TService service;

        public GenericDataServiceController(TService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        protected abstract IMapper Mapper { get; }

        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var data = await this.service.SelectAsync(null);
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
                var model = await this.service.GetByIdAsync(id);
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

        public async Task<IHttpActionResult> GetPaged(string sortKey, int skip = PagingConstants.DefaultSkip, int take = PagingConstants.DefaultTake)
        {
            if (string.IsNullOrWhiteSpace(sortKey))
            {
                return this.BadRequest(Messages.InvalidValueForSortKeyQueryParameterMessage);
            }

            try
            {
                var data = await this.service.SelectAsync(null, skip, take, sortKey);
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
        /// <param name="entity">Entity to be added.</param>
        /// <returns>OK if there is no errors; BadRequest on exception.</returns>
        public async Task<IHttpActionResult> Post(TRequestModel entity)
        {
            try
            {
                var item = this.Mapper.Map<TServiceModel>(entity);
                var result = await this.service.InsertAsync(item);
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
        /// <param name="entity">Entity to be updated.</param>
        /// <returns>OK if there is no errors; BadRequest on exception.</returns>
        public async Task<IHttpActionResult> Put(TRequestModel entity)
        {
            try
            {
                var item = this.Mapper.Map<TServiceModel>(entity);
                var result = await this.service.UpdateAsync(item);
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
        /// <param name="entity">Entity to be deleted.</param>
        /// <returns>OK if there is no errors; BadRequest on exception.</returns>
        public async Task<IHttpActionResult> Delete(TRequestModel entity)
        {
            try
            {
                var item = this.Mapper.Map<TServiceModel>(entity);
                var result = await this.service.DeleteAsync(item);
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
                var result = await this.service.DeleteAsync(id);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.ToString());
            }
        }
    }
}
