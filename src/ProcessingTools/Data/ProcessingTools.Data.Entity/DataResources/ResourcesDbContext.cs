// <copyright file="ResourcesDbContext.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.DataResources
{
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Data.Models.Entity.DataResources;

    public class ResourcesDbContext : DbContext
    {
        public ResourcesDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Abbreviation> Abbreviations { get; set; }

        public DbSet<ContentType> ContentTypes { get; set; }

        public DbSet<SourceId> Sources { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContentType>().HasIndex(m => m.Name).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
