// <copyright file="MediatypesDbSeeder.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DbSeeder.Seeders
{
    using ProcessingTools.Data.Entity.Files;
    using ProcessingTools.Data.Seed.Files;
    using ProcessingTools.DbSeeder.Abstractions.Seeders;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    /// <summary>
    /// Mediatypes database seeder.
    /// </summary>
    public class MediatypesDbSeeder : GenericDbSeeder<IMediatypesDataInitializer, IMediatypesDataSeeder>, IMediatypesDbSeeder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediatypesDbSeeder"/> class.
        /// </summary>
        /// <param name="initializer">Instance of <see cref="IMediatypesDataInitializer"/>.</param>
        /// <param name="seeder">Instance of <see cref="IMediatypesDataSeeder"/>.</param>
        public MediatypesDbSeeder(IMediatypesDataInitializer initializer, IMediatypesDataSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
