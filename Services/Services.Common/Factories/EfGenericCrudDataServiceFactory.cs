namespace ProcessingTools.Services.Common.Factories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using AutoMapper;
    using Contracts;
    using Models.Contracts;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public abstract class EfGenericCrudDataServiceFactory<TDbModel, TServiceModel, TOrderKey> : ICrudDataService<TServiceModel>
        where TDbModel : class
        where TServiceModel : ISimpleServiceModel
    {
        private readonly IMapper mapper;

        private Expression<Func<TDbModel, TOrderKey>> orderExpression;
        private IEfRepository<TDbModel> repository;

        public EfGenericCrudDataServiceFactory(IEfRepository<TDbModel> repository, Expression<Func<TDbModel, TOrderKey>> orderExpression)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (orderExpression == null)
            {
                throw new ArgumentNullException(nameof(orderExpression));
            }

            this.repository = repository;
            this.orderExpression = orderExpression;

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<TDbModel, TServiceModel>();
                c.CreateMap<TServiceModel, TDbModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        public void Add(TServiceModel model)
        {
            var entity = this.mapper.Map<TDbModel>(model);
            this.repository.Add(entity);
            this.repository.SaveChanges();
        }

        public void Delete(object id)
        {
            this.repository.Delete(id);
            this.repository.SaveChanges();
        }

        public void Delete(TServiceModel model)
        {
            var entity = this.repository.GetById(model.Id);
            this.repository.Delete(entity);
            this.repository.SaveChanges();
        }

        public virtual void Update(TServiceModel model)
        {
            var entity = this.repository.GetById(model.Id);
            entity = this.mapper.Map<TServiceModel, TDbModel>(model, entity);

            this.repository.Update(entity);
            this.repository.SaveChanges();
        }

        public virtual IQueryable<TServiceModel> All()
        {
            return this.Get(0, DefaultPagingConstants.MaximalItemsPerPageAllowed);
        }

        public virtual TServiceModel Get(object id)
        {
            var entity = this.repository.GetById(id);
            return this.mapper.Map<TServiceModel>(entity);
        }

        public virtual IQueryable<TServiceModel> Get(int skip, int take)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > DefaultPagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            return this.repository.All()
                .OrderByDescending(this.orderExpression)
                .Skip(skip)
                .Take(take)
                .ToList()
                .Select(this.mapper.Map<TServiceModel>)
                .AsQueryable();
        }
    }
}
