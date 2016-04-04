namespace ProcessingTools.Data.Miners.Tests.RegressionTests.Services.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
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
                var mockProduct = new Mock<ProductServiceModel>();
                mockProduct.Setup(m => m.Id).Returns(i);
                mockProduct.Setup(m => m.Name).Returns($"Product {i}");

                this.Items.Add(mockProduct.Object);
            }
        }

        public HashSet<ProductServiceModel> Items { get; set; }

        public void Add(ProductServiceModel entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductServiceModel> All()
        {
            return this.Items.AsQueryable();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(ProductServiceModel entity)
        {
            throw new NotImplementedException();
        }

        public ProductServiceModel Get(object id)
        {
            return this.Items.FirstOrDefault(p => p.Id == (int)id);
        }

        public IQueryable<ProductServiceModel> Get(int skip, int take)
        {
            return this.Items
                .OrderBy(p => p.Id)
                .Skip(skip)
                .Take(take)
                .AsQueryable();
        }

        public void Update(ProductServiceModel entity)
        {
            throw new NotImplementedException();
        }
    }
}