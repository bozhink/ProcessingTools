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
            this.Items = new HashSet<IProduct>();
            for (int i = 0; i < NumberOfDataItems; ++i)
            {
                var mockProduct = new Mock<IProduct>();
                mockProduct.Setup(m => m.Id).Returns(i);
                mockProduct.Setup(m => m.Name).Returns($"Product {i}");

                this.Items.Add(mockProduct.Object);
            }
        }

        public HashSet<IProduct> Items { get; set; }

        public void Add(IProduct entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<IProduct> All()
        {
            return this.Items.AsQueryable();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(IProduct entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<IProduct> Get(object id)
        {
            return this.Items
                .Where(p => p.Id == (int)id)
                .AsQueryable();
        }

        public IQueryable<IProduct> Get(int skip, int take)
        {
            return this.Items
                .OrderBy(p => p.Id)
                .Skip(skip)
                .Take(take)
                .AsQueryable();
        }

        public void Update(IProduct entity)
        {
            throw new NotImplementedException();
        }
    }
}