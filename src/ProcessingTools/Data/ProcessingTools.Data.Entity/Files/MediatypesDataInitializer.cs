// <copyright file="MediatypesDataInitializer.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Files
{
    using ProcessingTools.Data.Entity.Abstractions;

    public class MediatypesDataInitializer : DbContextInitializer<MediatypesDbContext>, IMediatypesDataInitializer
    {
        public MediatypesDataInitializer(MediatypesDbContext context)
            : base(context)
        {
        }

        protected override void SetInitializer()
        {
        }
    }
}
