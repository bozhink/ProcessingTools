namespace ProcessingTools.Web.Api.Factories
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using AutoMapper;

    using Common.Constants;
    using Services.Common.Contracts;

    public abstract class GenericDataServiceControllerFactory<TServiceModel, TRequestModel, TResponseModel> : ApiController
        where TServiceModel : class
        where TRequestModel : class
        where TResponseModel : class
    {
        private readonly IMapper mapper;

        private ICrudDataService<TServiceModel> service;

        public GenericDataServiceControllerFactory(ICrudDataService<TServiceModel> service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }

            this.service = service;

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<TServiceModel, TResponseModel>();
                c.CreateMap<TRequestModel, TServiceModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        public IHttpActionResult GetAll()
        {
            var result = this.service.All()
                .Select(this.mapper.Map<TResponseModel>)
                .ToList();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        public IHttpActionResult GetById(string id)
        {
            int parsedId;
            if (!int.TryParse(id, out parsedId))
            {
                return this.BadRequest("Invalid id.");
            }

            var result = this.service.Get(parsedId)
                .Select(this.mapper.Map<TResponseModel>)
                .FirstOrDefault();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        public IHttpActionResult GetPaged(string skip, string take = DefaultPagingConstants.DefaultTakeString)
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

            var result = this.service.Get(skipItemsCount, takeItemsCount)
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
        public IHttpActionResult Post(TRequestModel entity)
        {
            var item = this.mapper.Map<TServiceModel>(entity);
            try
            {
                this.service.Add(item);
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
        public IHttpActionResult Put(TRequestModel entity)
        {
            var item = this.mapper.Map<TServiceModel>(entity);
            try
            {
                this.service.Update(item);
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
        public IHttpActionResult Delete(TRequestModel entity)
        {
            var item = this.mapper.Map<TServiceModel>(entity);
            try
            {
                this.service.Delete(item);
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
        public IHttpActionResult Delete(string id)
        {
            int parsedId;
            if (!int.TryParse(id, out parsedId))
            {
                return this.BadRequest(id);
            }

            try
            {
                this.service.Delete(id);
            }
            catch
            {
                return this.BadRequest();
            }

            return this.Ok();
        }
    }
}