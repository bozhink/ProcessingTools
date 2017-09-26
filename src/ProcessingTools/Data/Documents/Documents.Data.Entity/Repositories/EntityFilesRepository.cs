namespace ProcessingTools.Documents.Data.Entity.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Common.Entity.Repositories;
    using ProcessingTools.Documents.Data.Entity.Contracts;
    using ProcessingTools.Documents.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Documents.Data.Entity.Models;
    using ProcessingTools.Models.Contracts.Documents;

    // TODO
    public class EntityFilesRepository : EntityRepository<DocumentsDbContext, File>, IEntityFilesRepository
    {
        private readonly IGuidProvider guidProvider;
        private readonly IDateTimeProvider dateTimeProvider;

        public EntityFilesRepository(
            IDocumentsDbContextProvider contextProvider,
            IGuidProvider guidProvider,
            IDateTimeProvider dateTimeProvider)
            : base(contextProvider)
        {
            this.guidProvider = guidProvider ?? throw new ArgumentNullException(nameof(guidProvider));
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public Task<object> AddAsync(IFile entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return Task.Run<object>(() =>
            {
                var id = this.guidProvider.NewGuid();
                var now = this.dateTimeProvider.Now;

                var dbentity = new File
                {
                    Id = id,
                    DateCreated = now,
                    DateModified = now,
                    ContentLength = entity.ContentLength,
                    ContentType = entity.ContentType,
                    CreatedByUser = entity.CreatedByUser,
                    Description = entity.Description,
                    FileExtension = entity.FileExtension,
                    FileName = entity.FileName,
                    FullName = entity.FullName,
                    ModifiedByUser = entity.ModifiedByUser,
                    OriginalContentLength = entity.ContentLength,
                    OriginalContentType = entity.ContentType,
                    OriginalFileExtension = entity.FileExtension,
                    OriginalFileName = entity.FileName
                };

                var entry = this.GetEntry(dbentity);
                if (entry.State != EntityState.Detached)
                {
                    entry.State = EntityState.Added;
                    return dbentity;
                }
                else
                {
                    return this.DbSet.Add(dbentity);
                }
            });
        }

        public Task<IFile> GetAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return Task.Run<IFile>(() =>
            {
                var dbentity = this.DbSet.Find(id);
                return dbentity;
            });
        }

        public Task<object> RemoveAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<object> UpdateAsync(IFile entity)
        {
            throw new NotImplementedException();
        }
    }
}
