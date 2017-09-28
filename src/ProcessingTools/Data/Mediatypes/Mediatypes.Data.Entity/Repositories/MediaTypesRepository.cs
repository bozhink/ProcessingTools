namespace ProcessingTools.Mediatypes.Data.Entity.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Contracts.Repositories.Mediatypes;
    using ProcessingTools.Mediatypes.Data.Entity.Contracts;
    using ProcessingTools.Mediatypes.Data.Entity.Models;
    using ProcessingTools.Models.Contracts.Mediatypes;

    public class MediatypesRepository : IMediatypesRepository, ISearchableMediatypesRepository, IDisposable
    {
        private readonly IMediatypesDbContext db;

        public MediatypesRepository(IMediatypesDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        ~MediatypesRepository()
        {
            this.Dispose(false);
        }

        public async Task<object> Add(IMediatypeEntity mediatype)
        {
            if (mediatype == null)
            {
                throw new ArgumentNullException(nameof(mediatype));
            }

            var mimetype = await this.db.Mimetypes
                .FirstOrDefaultAsync(m => m.Name.ToLower() == mediatype.Mimetype.ToLower())
                .ConfigureAwait(false);

            if (mimetype == null)
            {
                mimetype = new Mimetype
                {
                    Name = mediatype.Mimetype.ToLower()
                };
            }

            var mimesubtype = await this.db.Mimesubtypes
                .FirstOrDefaultAsync(s => s.Name.ToLower() == mediatype.Mimesubtype.ToLower())
                .ConfigureAwait(false);

            if (mimesubtype == null)
            {
                mimesubtype = new Mimesubtype
                {
                    Name = mediatype.Mimesubtype.ToLower()
                };
            }

            var mimetypePair = await this.db.MimetypePairs
                .FirstOrDefaultAsync(p => p.Mimetype == mimetype && p.Mimesubtype == mimesubtype)
                .ConfigureAwait(false);

            if (mimetypePair == null)
            {
                mimetypePair = new MimetypePair
                {
                    Mimetype = mimetype,
                    Mimesubtype = mimesubtype
                };
            }

            var entity = await this.db.FileExtensions
                .FirstOrDefaultAsync(e => e.Name.ToLower() == mediatype.FileExtension.ToLower())
                .ConfigureAwait(false);

            if (entity == null)
            {
                entity = new FileExtension
                {
                    Name = mediatype.FileExtension.ToLower()
                };
            }

            entity.Description = mediatype.Description;
            entity.MimetypePairs.Add(mimetypePair);

            var entry = this.db.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.db.FileExtensions.Add(entity);
            }

            return true;
        }

        public IEnumerable<IMediatypeEntity> GetByFileExtension(string fileExtension)
        {
            if (string.IsNullOrWhiteSpace(fileExtension))
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }

            var result = this.db.FileExtensions
                .Where(e => e.Name.ToLower() == fileExtension.ToLower())
                .SelectMany(e => e.MimetypePairs.Select(p => new Mediatype
                {
                    FileExtension = e.Name,
                    Description = e.Description,
                    Mimetype = p.Mimetype.Name,
                    Mimesubtype = p.Mimesubtype.Name
                }))
                .AsEnumerable<IMediatypeEntity>();

            return result;
        }

        public async Task<object> Remove(string fileExtension)
        {
            if (string.IsNullOrWhiteSpace(fileExtension))
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }

            var entity = await this.db.FileExtensions
                .FirstOrDefaultAsync(e => e.Name.ToLower() == fileExtension.ToLower())
                .ConfigureAwait(false);

            if (entity == null)
            {
                return false;
            }

            entity.MimetypePairs.Clear();

            var entry = this.db.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.db.FileExtensions.Attach(entity);
                this.db.FileExtensions.Remove(entity);
            }

            return true;
        }

        public async Task<long> SaveChanges() => await this.db.SaveChangesAsync().ConfigureAwait(false);

        public async Task<object> UpdateDescription(string fileExtension, string description)
        {
            if (string.IsNullOrWhiteSpace(fileExtension))
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }

            var entity = await this.db.FileExtensions
                .FirstOrDefaultAsync(e => e.Name.ToLower() == fileExtension.ToLower())
                .ConfigureAwait(false);

            if (entity == null)
            {
                return false;
            }

            entity.Description = description;

            var entry = this.db.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.db.FileExtensions.Attach(entity);
            }

            entry.State = EntityState.Modified;

            return true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
        }
    }
}
