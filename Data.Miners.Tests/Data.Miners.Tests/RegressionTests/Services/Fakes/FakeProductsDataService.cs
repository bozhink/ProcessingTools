namespace ProcessingTools.Data.Miners.Tests.RegressionTests.Services.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ProcessingTools.Services.Data.Contracts;
    using ProcessingTools.Services.Data.Models;

    public class FakeProductsDataService : IProductsDataService
    {
        private const int NumberOfDataItems = 500;

        public FakeProductsDataService()
        {
            this.Items = new HashSet<ProductServiceModel>();
            for (int i = 0; i < NumberOfDataItems; ++i)
            {
                this.Items.Add(new ProductServiceModel
                {
                    Id = i,
                    Name = $"Product {i}"
                });
            }
        }

        public HashSet<ProductServiceModel> Items { get; set; }

        public Task<int> Add(params ProductServiceModel[] entities)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<ProductServiceModel>> All()
        {
            return Task.FromResult(this.Items.AsQueryable());
        }

        public Task<int> Delete(params object[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(params ProductServiceModel[] entities)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<ProductServiceModel>> Get(params object[] ids)
        {
            var result = ids.Select(id => this.Items.FirstOrDefault(p => p.Id == (int)id)).AsQueryable();

            return Task.FromResult(result);
        }

        public Task<IQueryable<ProductServiceModel>> All(int skip, int take)
        {
            return Task.FromResult(this.Items
                .OrderBy(p => p.Id)
                .Skip(skip)
                .Take(take)
                .AsQueryable());
        }

        public Task<int> Update(params ProductServiceModel[] entities)
        {
            throw new NotImplementedException();
        }
    }
}