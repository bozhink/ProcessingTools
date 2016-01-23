namespace ProcessingTools.Data.Miners.Tests.RegressionTests.Services.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using ProcessingTools.Services.Data.Contracts;
    using ProcessingTools.Services.Data.Models.Contracts;

    public class FakeInstitutionsDataService : IInstitutionsDataService
    {
        private const int NumberOfDataItems = 500;

        public FakeInstitutionsDataService()
        {
            this.Items = new HashSet<IInstitution>();
            for (int i = 0; i < NumberOfDataItems; ++i)
            {
                var mockProduct = new Mock<IInstitution>();
                mockProduct.Setup(m => m.Id).Returns(i);
                mockProduct.Setup(m => m.Name).Returns($"Institution {i}");

                this.Items.Add(mockProduct.Object);
            }
        }

        public HashSet<IInstitution> Items { get; set; }

        public void Add(IInstitution entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<IInstitution> All()
        {
            return this.Items.AsQueryable();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(IInstitution entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<IInstitution> Get(object id)
        {
            return this.Items
                .Where(p => p.Id == (int)id)
                .AsQueryable();
        }

        public IQueryable<IInstitution> Get(int skip, int take)
        {
            return this.Items
                .OrderBy(p => p.Id)
                .Skip(skip)
                .Take(take)
                .AsQueryable();
        }

        public void Update(IInstitution entity)
        {
            throw new NotImplementedException();
        }
    }
}