namespace ProcessingTools.Data.Entity.Documents
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Entity.Abstractions;
    using ProcessingTools.Data.Models.Entity.Documents;
    using ProcessingTools.Models.Contracts.Documents;

    // TODO
    public class EntityFilesRepository : EntityRepository<DocumentsDbContext, File>, IEntityFilesRepository
    {
        private readonly IApplicationContext applicationContext;

        public EntityFilesRepository(IDbContextProvider<DocumentsDbContext> contextProvider, IApplicationContext applicationContext)
            : base(contextProvider)
        {
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        public Task<object> AddAsync(IFile entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return Task.Run<object>(() =>
            {
                var id = this.applicationContext.GuidProvider.Invoke();
                var now = this.applicationContext.DateTimeProvider.Invoke();

                var dbentity = new File
                {
                    Id = id,
                    CreatedOn = now,
                    ModifiedOn = now,
                    ContentLength = entity.ContentLength,
                    ContentType = entity.ContentType,
                    CreatedBy = entity.CreatedBy,
                    Description = entity.Description,
                    FileExtension = entity.FileExtension,
                    FileName = entity.FileName,
                    FullName = entity.FullName,
                    ModifiedBy = entity.ModifiedBy,
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
