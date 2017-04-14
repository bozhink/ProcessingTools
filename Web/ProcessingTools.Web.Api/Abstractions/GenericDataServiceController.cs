namespace ProcessingTools.Web.Api.Abstractions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using AutoMapper;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Services.Data;

    public class GenericDataServiceController<TServiceModel, TRequestModel, TResponseModel> : ApiController
        where TServiceModel : class
        where TRequestModel : class
        where TResponseModel : class
    {
        private readonly IMapper mapper;

        private readonly IMultiEntryDataService<TServiceModel> service;

        public GenericDataServiceController(IMultiEntryDataService<TServiceModel> service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<TServiceModel, TResponseModel>();
                c.CreateMap<TRequestModel, TServiceModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        public async Task<IHttpActionResult> GetAll()
        {
            var result = (await this.service.SelectAllAsync())
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
            int parsedId;
            if (!int.TryParse(id, out parsedId))
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

        public async Task<IHttpActionResult> GetPaged(string skip, string take = PagingConstants.DefaultTakeString)
        {
            int skipItemsCount;
            if (!int.TryParse(skip, out skipItemsCount))
            {
                return this.BadRequest(Messages.InvalidValueForSkipQueryParameterMessage);
            }

            int takeItemsCount;
            if (!int.TryParse(take, out takeItemsCount))
            {
                return this.BadRequest(Messages.InvalidValueForTakeQueryParameterMessage);
            }

            var result = (await this.service.SelectAsync(skipItemsCount, takeItemsCount))
                .Select(this.mapper.Map<TResponseModel>)
                .ToList();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
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
            int parsedId;
            if (!int.TryParse(id, out parsedId))
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
