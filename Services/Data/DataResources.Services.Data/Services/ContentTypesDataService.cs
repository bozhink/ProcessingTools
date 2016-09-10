namespace ProcessingTools.DataResources.Services.Data
{
    using Contracts;
    using Models.Contracts;
    using Models;
    using ProcessingTools.Common.Validation;
    using ProcessingTools.DataResources.Data.Entity.Contracts;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;

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
    }
}
