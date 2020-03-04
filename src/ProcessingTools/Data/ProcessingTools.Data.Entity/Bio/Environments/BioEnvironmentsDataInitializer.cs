// <copyright file="BioEnvironmentsDataInitializer.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Bio.Environments
{
    using ProcessingTools.Data.Entity.Abstractions;

    /// <summary>
    /// Bio environments database initializer.
    /// </summary>
    public class BioEnvironmentsDataInitializer : DbContextInitializer<BioEnvironmentsDbContext>, IBioEnvironmentsDataInitializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BioEnvironmentsDataInitializer"/> class.
        /// </summary>
        /// <param name="context">Instanc e of <see cref="BioEnvironmentsDbContext"/>.</param>
        public BioEnvironmentsDataInitializer(BioEnvironmentsDbContext context)
            : base(context)
        {
        }

        /// <inheritdoc/>
        protected override void SetInitializer()
        {
        }
    }
}
