namespace ProcessingTools.Resources.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Models;
    using Models.Contracts;

    using ProcessingTools.Common.Validation;
    using ProcessingTools.Resources.Data.Entity.Contracts;
    using ProcessingTools.Resources.Data.Entity.Models;

    public class ContentTypesDataService : IContentTypesDataService
    {
        private readonly IDataResourcesDbContextProvider contextProvider;

        public ContentTypesDataService(IDataResourcesDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        public async Task<object> Add(IContentTypeCreateServiceModel model)
        {
            DummyValidator.ValidateModel(model);

            int result = 0;

            using (var db = this.contextProvider.Create())
            {
                var entity = new ContentType
                {
                    Name = model.Name
                };

                db.ContentTypes.Add(entity);
                result = await db.SaveChangesAsync();
            }

            return result;
        }

        public async Task<IEnumerable<IContentTypeServiceModel>> All()
        {
            IEnumerable<IContentTypeServiceModel> result = null;

            using (var db = this.contextProvider.Create())
            {
                var query = db.ContentTypes
                    .OrderBy(e => e.Name)
                    .Select(e => new ContentTypeServiceModel
                    {
                        Id = e.Id,
                        Name = e.Name
                    });

                result = await query.ToListAsync();
            }

            return result;
        }

        public async Task<IEnumerable<IContentTypeServiceModel>> All(int pageNumber, int numberOfItemsPerPage)
        {
            ValidationHelpers.ValidatePageNumber(pageNumber);
            ValidationHelpers.ValidateNumberOfItemsPerPage(numberOfItemsPerPage);

            IEnumerable<IContentTypeServiceModel> result = null;

            using (var db = this.contextProvider.Create())
            {
                var query = db.ContentTypes
                    .OrderBy(e => e.Name)
                    .Skip(pageNumber * numberOfItemsPerPage)
                    .Take(numberOfItemsPerPage)
                    .Select(e => new ContentTypeServiceModel
                    {
                        Id = e.Id,
                        Name = e.Name
                    });

                result = await query.ToListAsync();
            }

            return result;
        }

        public async Task<long> Count()
        {
            long result = 0L;
            using (var db = this.contextProvider.Create())
            {
                result = await db.ContentTypes.LongCountAsync();
            }

            return result;
        }

        public async Task<object> Delete(object id)
        {
            DummyValidator.ValidateId(id);

            int result = 0;
            using (var db = this.contextProvider.Create())
            {
                var entity = db.ContentTypes.Find(id);
                db.ContentTypes.Remove(entity);
                result = await db.SaveChangesAsync();
            }

            return result;
        }

        public async Task<IContentTypeDetailsServiceModel> GetDetails(object id)
        {
            DummyValidator.ValidateId(id);

            IContentTypeDetailsServiceModel result = null;
            using (var db = this.contextProvider.Create())
            {
                var query = db.ContentTypes
                    .Where(e => e.Id.ToString() == id.ToString())
                    .Select(e => new ContentTypeDetailsServiceModel
                    {
                        Id = e.Id,
                        Name = e.Name
                    });

                result = await query.FirstOrDefaultAsync();
            }

            return result;
        }

        public async Task<object> Update(IContentTypeUpdateServiceModel model)
        {
            DummyValidator.ValidateModel(model);

            int result = 0;
            using (var db = this.contextProvider.Create())
            {
                var entity = new ContentType
                {
                    Id = model.Id,
                    Name = model.Name
                };

                db.Entry(entity).State = EntityState.Modified;
                result = await db.SaveChangesAsync();
            }

            return result;
        }
    }
}
