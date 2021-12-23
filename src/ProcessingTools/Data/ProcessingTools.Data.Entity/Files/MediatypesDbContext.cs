// <copyright file="MediatypesDbContext.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Files
{
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Data.Models.Entity.Files;

    public class MediatypesDbContext : DbContext
    {
        public MediatypesDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<FileExtension> FileExtensions { get; set; }

        public DbSet<Mimetype> Mimetypes { get; set; }

        public DbSet<Mimesubtype> Mimesubtypes { get; set; }

        public DbSet<MimetypePair> MimetypePairs { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
