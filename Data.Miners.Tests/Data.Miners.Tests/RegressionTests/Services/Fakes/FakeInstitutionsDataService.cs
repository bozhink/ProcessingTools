namespace ProcessingTools.Data.Miners.Tests.RegressionTests.Services.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using ProcessingTools.Services.Data.Contracts;
    using ProcessingTools.Services.Data.Models;

    public class FakeInstitutionsDataService : IInstitutionsDataService
    {
        private const int NumberOfDataItems = 500;

        public FakeInstitutionsDataService()
        {
            this.Items = new HashSet<InstitutionServiceModel>();
            for (int i = 0; i < NumberOfDataItems; ++i)
            {
                var mockProduct = new Mock<InstitutionServiceModel>();
                mockProduct.Setup(m => m.Id).Returns(i);
                mockProduct.Setup(m => m.Name).Returns($"Institution {i}");

                this.Items.Add(mockProduct.Object);
            }
        }

        public HashSet<InstitutionServiceModel> Items { get; set; }

        public void Add(InstitutionServiceModel entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<InstitutionServiceModel> All()
        {
            return this.Items.AsQueryable();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(InstitutionServiceModel entity)
        {
            throw new NotImplementedException();
        }

        public InstitutionServiceModel Get(object id)
        {
            return this.Items.FirstOrDefault(p => p.Id == (int)id);
        }

        public IQueryable<InstitutionServiceModel> Get(int skip, int take)
        {
            return this.Items
                .OrderBy(p => p.Id)
                .Skip(skip)
                .Take(take)
                .AsQueryable();
        }

        public void Update(InstitutionServiceModel entity)
        {
            throw new NotImplementedException();
        }
    }
}