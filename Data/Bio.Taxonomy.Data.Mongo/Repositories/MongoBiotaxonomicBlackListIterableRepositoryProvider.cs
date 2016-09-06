namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories
{
    using System;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class MongoBiotaxonomicBlackListIterableRepositoryProvider : IMongoBiotaxonomicBlackListIterableRepositoryProvider
    {
        private readonly IBiotaxonomyMongoDatabaseProvider provider;

        public MongoBiotaxonomicBlackListIterableRepositoryProvider(IBiotaxonomyMongoDatabaseProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.provider = provider;
        }

        public IIterableRepository<IBlackListEntity> Create()
        {
            return new MongoBiotaxonomicBlackListIterableRepository(this.provider);
        }
    }
}
