namespace ProcessingTools.Mediatypes.Data.Entity.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using ProcessingTools.Mediatypes.Data.Common.Contracts.Models;
    using ProcessingTools.Mediatypes.Data.Common.Contracts.Repositories;
    using ProcessingTools.Mediatypes.Data.Common.Models;

    public class MediatypesRepository : IMediatypesRepository, IDisposable
    {
        private readonly IMediatypesDbContext db;

        public MediatypesRepository(IMediatypesDbContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException(nameof(db));
            }

            this.db = db;
        }

        public async Task<IEnumerable<IMediatype>> GetByFileExtension(string fileExtension)
        {
            if (string.IsNullOrWhiteSpace(fileExtension))
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }

            var result = await this.db.FileExtensions
                .Where(e => e.Name.ToLower() == fileExtension.ToLower())
                .SelectMany(e => e.MimeTypePairs.Select(p => new Mediatype
                {
                    FileExtension = e.Name,
                    Description = e.Description,
                    Mimetype = p.Mimetype.Name,
                    Mimesubtype = p.Mimesubtype.Name
                }))
                .ToListAsync<IMediatype>();

            return result;
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
