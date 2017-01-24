namespace ProcessingTools.Mediatypes.Data.Mongo.Repositories
{
    using ProcessingTools.Mediatypes.Data.Common.Contracts.Models;
    using ProcessingTools.Mediatypes.Data.Common.Contracts.Repositories;
    using System;
    using System.Collections.Generic;

    public class MongoMediatypesSearchableRepository : ISearchableMediatypesRepository
    {
        public IEnumerable<IMediatype> GetByFileExtension(string fileExtension)
        {


            throw new NotImplementedException();
        }
    }
}
