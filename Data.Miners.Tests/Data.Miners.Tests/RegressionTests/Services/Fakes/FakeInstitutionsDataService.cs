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
            this.Items = new HashSet<IInstitutionServiceModel>();
            for (int i = 0; i < NumberOfDataItems; ++i)
            {
                var mockProduct = new Mock<IInstitutionServiceModel>();
                mockProduct.Setup(m => m.Id).Returns(i);
                mockProduct.Setup(m => m.Name).Returns($"Institution {i}");

                this.Items.Add(mockProduct.Object);
            }
        }

        public HashSet<IInstitutionServiceModel> Items { get; set; }

        public void Add(IInstitutionServiceModel entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<IInstitutionServiceModel> All()
        {
            return this.Items.AsQueryable();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(IInstitutionServiceModel entity)
        {
            throw new NotImplementedException();
        }

        public IInstitutionServiceModel Get(object id)
        {
            return this.Items.FirstOrDefault(p => p.Id == (int)id);
        }

        public IQueryable<IInstitutionServiceModel> Get(int skip, int take)
        {
            return this.Items
                .OrderBy(p => p.Id)
                .Skip(skip)
                .Take(take)
                .AsQueryable();
        }

        public void Update(IInstitutionServiceModel entity)
        {
            throw new NotImplementedException();
        }
    }
}