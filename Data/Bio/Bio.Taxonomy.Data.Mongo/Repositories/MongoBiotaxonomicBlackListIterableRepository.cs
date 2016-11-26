namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories
{
    using System.Collections.Generic;

    using Contracts;
    using Models;

    using MongoDB.Driver;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts;
    using ProcessingTools.Data.Common.Mongo.Repositories;

    public class MongoBiotaxonomicBlackListIterableRepository : MongoRepository<MongoBlackListEntity>, IMongoBiotaxonomicBlackListIterableRepository
    {
        public MongoBiotaxonomicBlackListIterableRepository(IBiotaxonomyMongoDatabaseProvider provider)
            : base(provider)
        {
        }

        public IEnumerable<IBlackListEntity> Entities => this.Collection
            .Find("{}")
            .ToCursor()
            .ToEnumerable<IBlackListEntity>();
    }
}
