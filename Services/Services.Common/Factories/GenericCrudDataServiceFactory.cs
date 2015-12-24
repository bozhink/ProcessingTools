namespace ProcessingTools.Services.Common.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using AutoMapper;
    using Contracts;
    using Models.Contracts;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Data.Common.Repositories;

    public abstract class GenericCrudDataServiceFactory<TDbModel, TServiceModel, TOrderKey> : ICrudDataService<TServiceModel>
        where TDbModel : class
        where TServiceModel : IDataServiceModel
    {
        private IRepository<TDbModel> repository;
        private Expression<Func<TDbModel, TOrderKey>> orderExpression;

        public GenericCrudDataServiceFactory(IRepository<TDbModel> repository, Expression<Func<TDbModel, TOrderKey>> orderExpression)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            if (orderExpression == null)
            {
                throw new ArgumentNullException("orderExpression");
            }

            this.repository = repository;
            this.orderExpression = orderExpression;
        }

        public void Add(TServiceModel entity)
        {
            var item = Mapper.Map<TDbModel>(entity);
            this.repository.Add(item);
            this.repository.SaveChanges();
        }

        public IQueryable<TServiceModel> All()
        {
            return this.repository.All()
                .OrderByDescending(this.orderExpression)
                .ToList()
                .Select(Mapper.Map<TServiceModel>)
                .AsQueryable();
        }

        public void Delete(object id)
        {
            this.repository.Delete(id);
            this.repository.SaveChanges();
        }

        public void Delete(TServiceModel entity)
        {
            var item = this.repository.GetById(entity.Id);
            this.repository.Delete(item);
            this.repository.SaveChanges();
        }

        public IQueryable<TServiceModel> Get(object id)
        {
            var item = this.repository.GetById(id);
            return new List<TServiceModel>
            {
                Mapper.Map<TServiceModel>(item)
            }
            .AsQueryable();
        }

        public IQueryable<TServiceModel> Get(int skip, int take)
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
                .Select(Mapper.Map<TServiceModel>)
                .AsQueryable();
        }

        public void Update(TServiceModel entity)
        {
            var item = this.repository.GetById(entity.Id);
            item = Mapper.Map<TServiceModel, TDbModel>(entity, item);

            this.repository.Update(item);
            this.repository.SaveChanges();
        }
    }
}
