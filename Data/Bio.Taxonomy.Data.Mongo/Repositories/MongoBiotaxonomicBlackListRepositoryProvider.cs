namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories
{
    using System;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts;
    using ProcessingTools.Contracts.Data.Repositories;

    public class MongoBiotaxonomicBlackListRepositoryProvider : IMongoBiotaxonomicBlackListRepositoryProvider
    {
        private readonly IBiotaxonomyMongoDatabaseProvider provider;

        public MongoBiotaxonomicBlackListRepositoryProvider(IBiotaxonomyMongoDatabaseProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.provider = provider;
        }

        public ICrudRepository<IBlackListEntity> Create()
        {
            return new MongoBiotaxonomicBlackListRepository(this.provider);
        }
    }
}
