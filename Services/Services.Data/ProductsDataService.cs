namespace ProcessingTools.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Common.Exceptions;
    using Contracts;
    using Models;
    using Models.Contracts;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Data.Models;
    using ProcessingTools.Data.Repositories;

    public class ProductsDataService : IProductsDataService
    {
        private IDataRepository<Product> repository;

        public ProductsDataService(IDataRepository<Product> repository)
        {
            this.repository = repository;
        }

        public void Add(IProduct entity)
        {
            this.repository.Add(new Product
            {
                Name = entity.Name
            });

            this.repository.SaveChanges();
        }

        public IQueryable<IProduct> All()
        {
            return this.repository.All()
                .OrderByDescending(i => i.Name.Length)
                .Select(p => new ProductResponseModel
                {
                    Id = p.Id,
                    Name = p.Name
                });
        }

        public void Delete(object id)
        {
            this.repository.Delete(id);
            this.repository.SaveChanges();
        }

        public void Delete(IProduct entity)
        {
            var item = this.repository.GetById(entity.Id);
            this.repository.Delete(item);
            this.repository.SaveChanges();
        }

        public IQueryable<IProduct> Get(object id)
        {
            var item = this.repository.GetById(id);
            return new List<IProduct>
            {
                new ProductResponseModel
                {
                    Id = item.Id,
                    Name = item.Name
                }
            }
            .AsQueryable();
        }

        public IQueryable<IProduct> Get(int skip, int take)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > DefaultPagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            var result = this.repository.All()
                .OrderByDescending(i => i.Name.Length)
                .Skip(skip)
                .Take(take)
                .Select(p => new ProductResponseModel
                {
                    Id = p.Id,
                    Name = p.Name
                });

            return result;
        }

        public void Update(IProduct entity)
        {
            var item = this.repository.GetById(entity.Id);

            item.Name = entity.Name;

            this.repository.Update(item);
            this.repository.SaveChanges();
        }
    }
}