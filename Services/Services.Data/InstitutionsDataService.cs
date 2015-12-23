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

    public class InstitutionsDataService : IInstitutionsDataService
    {
        private IDataRepository<Institution> repository;

        public InstitutionsDataService(IDataRepository<Institution> repository)
        {
            this.repository = repository;
        }

        public void Add(IInstitution entity)
        {
            this.repository.Add(new Institution
            {
                Name = entity.Name
            });

            this.repository.SaveChanges();
        }

        public IQueryable<IInstitution> All()
        {
            return this.repository.All()
                .OrderByDescending(i => i.Name.Length)
                .Select(p => new InstitutionResponseModel
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

        public void Delete(IInstitution entity)
        {
            var item = this.repository.GetById(entity.Id);
            this.repository.Delete(item);
            this.repository.SaveChanges();
        }

        public IQueryable<IInstitution> Get(object id)
        {
            var item = this.repository.GetById(id);
            return new List<IInstitution>
            {
                new InstitutionResponseModel
                {
                    Id = item.Id,
                    Name = item.Name
                }
            }
            .AsQueryable();
        }

        public IQueryable<IInstitution> Get(int skip, int take)
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
                .Select(p => new InstitutionResponseModel
                {
                    Id = p.Id,
                    Name = p.Name
                });

            return result;
        }

        public void Update(IInstitution entity)
        {
            var item = this.repository.GetById(entity.Id);

            item.Name = entity.Name;

            this.repository.Update(item);
            this.repository.SaveChanges();
        }
    }
}