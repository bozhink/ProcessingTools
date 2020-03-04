// <copyright file="BioEnvironmentsRepository.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Bio.Environments
{
    using ProcessingTools.Data.Entity.Abstractions;

    /// <summary>
    /// Bio environments repository.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public class BioEnvironmentsRepository<T> : EntityRepository<BioEnvironmentsDbContext, T>, IBioEnvironmentsRepository<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BioEnvironmentsRepository{T}"/> class.
        /// </summary>
        /// <param name="context">Instance of <see cref="BioEnvironmentsDbContext"/>.</param>
        public BioEnvironmentsRepository(BioEnvironmentsDbContext context)
            : base(context)
        {
        }
    }
}
