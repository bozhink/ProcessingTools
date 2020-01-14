// <copyright file="BioEnvironmentsDbContext.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Bio.Environments
{
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Common.Constants.Data.Bio.Environments;
    using ProcessingTools.Data.Models.Entity.Bio.Environments;

    /// <summary>
    /// Bio Environments DbContext.
    /// </summary>
    public class BioEnvironmentsDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BioEnvironmentsDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public BioEnvironmentsDbContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the set of ENVO entities.
        /// </summary>
        public DbSet<EnvoEntity> EnvoEntities { get; set; }

        /// <summary>
        /// Gets or sets the set of ENVO globals.
        /// </summary>
        public DbSet<EnvoGlobal> EnvoGlobals { get; set; }

        /// <summary>
        /// Gets or sets the set of ENVO groups.
        /// </summary>
        public DbSet<EnvoGroup> EnvoGroups { get; set; }

        /// <summary>
        /// Gets or sets the set of ENVO names.
        /// </summary>
        public DbSet<EnvoName> EnvoNames { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnvoName>(b =>
            {
                b.Property(m => m.Id).HasMaxLength(ValidationConstants.MaximalLengthOfEnvoEntityId).IsRequired();
                b.Property(m => m.Value).HasMaxLength(ValidationConstants.MaximalLengthOfEnvoNameValue).IsRequired();
                b.Property(m => m.EntityId).HasMaxLength(ValidationConstants.MaximalLengthOfEnvoEntityId).IsRequired();

                b.HasKey(m => m.Id);

                b.HasIndex(m => m.EntityId);

                b.HasOne(m => m.Entity).WithMany(m => m.Names).HasForeignKey(m => m.EntityId);
            });

            modelBuilder.Entity<EnvoGroup>(b =>
            {
                b.Property(m => m.Entity1Id).HasMaxLength(ValidationConstants.MaximalLengthOfEnvoEntityId).IsRequired();
                b.Property(m => m.Entity2Id).HasMaxLength(ValidationConstants.MaximalLengthOfEnvoEntityId).IsRequired();

                b.HasKey(m => new { m.Entity1Id, m.Entity2Id });

                b.HasOne(m => m.Entity1).WithMany(m => m.Groups1).HasForeignKey(m => m.Entity1Id);
                b.HasOne(m => m.Entity2).WithMany(m => m.Groups2).HasForeignKey(m => m.Entity2Id);
            });

            modelBuilder.Entity<EnvoEntity>(b =>
            {
                b.Property(m => m.Id).HasMaxLength(ValidationConstants.MaximalLengthOfEnvoEntityId).IsRequired();
                b.Property(m => m.EnvoId).HasMaxLength(ValidationConstants.MaximalLengthOfEnvoId).IsRequired();

                b.HasKey(m => m.Id);

                b.HasMany(m => m.Names).WithOne(m => m.Entity).HasForeignKey(m => m.EntityId);
                b.HasMany(m => m.Groups1).WithOne(m => m.Entity1).HasForeignKey(m => m.Entity1Id);
                b.HasMany(m => m.Groups2).WithOne(m => m.Entity2).HasForeignKey(m => m.Entity2Id);
            });

            modelBuilder.Entity<EnvoGlobal>(b =>
            {
                b.Property(m => m.Value).HasMaxLength(ValidationConstants.MaximalLengthOfEnvoGlobalValue).IsRequired();
                b.Property(m => m.Status).HasMaxLength(ValidationConstants.MaximalLengthOfEnvoGlobalStatus);

                b.HasKey(m => m.Value);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
