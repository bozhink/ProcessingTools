namespace ProcessingTools.Data.Miners.Tests.RegressionTests.Services.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using ProcessingTools.Services.Data.Contracts;
    using ProcessingTools.Services.Data.Models.Contracts;

    public class FakeProductsDataService : IProductsDataService
    {
        private const int NumberOfDataItems = 500;

        public FakeProductsDataService()
        {
            this.Items = new HashSet<IProductServiceModel>();
            for (int i = 0; i < NumberOfDataItems; ++i)
            {
                var mockProduct = new Mock<IProductServiceModel>();
                mockProduct.Setup(m => m.Id).Returns(i);
                mockProduct.Setup(m => m.Name).Returns($"Product {i}");

                this.Items.Add(mockProduct.Object);
            }
        }

        public HashSet<IProductServiceModel> Items { get; set; }

        public void Add(IProductServiceModel entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<IProductServiceModel> All()
        {
            return this.Items.AsQueryable();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(IProductServiceModel entity)
        {
            throw new NotImplementedException();
        }

        public IProductServiceModel Get(object id)
        {
            return this.Items.FirstOrDefault(p => p.Id == (int)id);
        }

        public IQueryable<IProductServiceModel> Get(int skip, int take)
        {
            return this.Items
                .OrderBy(p => p.Id)
                .Skip(skip)
                .Take(take)
                .AsQueryable();
        }

        public void Update(IProductServiceModel entity)
        {
            throw new NotImplementedException();
        }
    }
}