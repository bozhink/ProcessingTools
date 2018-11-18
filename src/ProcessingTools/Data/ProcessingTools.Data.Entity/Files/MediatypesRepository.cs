namespace ProcessingTools.Data.Entity.Files
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Data.Contracts.Files;
    using ProcessingTools.Data.Models.Entity.Files;
    using ProcessingTools.Models.Contracts.Files.Mediatypes;

    public class MediatypesRepository : IMediatypesRepository, ISearchableMediatypesRepository, IDisposable
    {
        private readonly MediatypesDbContext db;

        public MediatypesRepository(MediatypesDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        ~MediatypesRepository()
        {
            this.Dispose(false);
        }

        public async Task<object> Add(IMediatypeBaseModel mediatype)
        {
            if (mediatype == null)
            {
                throw new ArgumentNullException(nameof(mediatype));
            }

            var mimetype = await this.db.Mimetypes
                .FirstOrDefaultAsync(m => m.Name.ToLower() == mediatype.MimeType.ToLower())
                .ConfigureAwait(false);

            if (mimetype == null)
            {
                mimetype = new Mimetype
                {
                    Name = mediatype.MimeType.ToLower()
                };
            }

            var mimesubtype = await this.db.Mimesubtypes
                .FirstOrDefaultAsync(s => s.Name.ToLower() == mediatype.MimeSubtype.ToLower())
                .ConfigureAwait(false);

            if (mimesubtype == null)
            {
                mimesubtype = new Mimesubtype
                {
                    Name = mediatype.MimeSubtype.ToLower()
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
                .FirstOrDefaultAsync(e => e.Name.ToLower() == mediatype.Extension.ToLower())
                .ConfigureAwait(false);

            if (entity == null)
            {
                entity = new FileExtension
                {
                    Name = mediatype.Extension.ToLower()
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

        public IEnumerable<IMediatypeBaseModel> GetByFileExtension(string fileExtension)
        {
            if (string.IsNullOrWhiteSpace(fileExtension))
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }

            var result = this.db.FileExtensions
                .Where(e => e.Name.ToLower() == fileExtension.ToLower())
                .SelectMany(e => e.MimetypePairs.Select(p => new Mediatype
                {
                    Extension = e.Name,
                    Description = e.Description,
                    MimeType = p.Mimetype.Name,
                    MimeSubtype = p.Mimesubtype.Name
                }))
                .AsEnumerable<IMediatypeBaseModel>();

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
