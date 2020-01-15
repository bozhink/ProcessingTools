// <copyright file="ICommandNamesProvider.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DbSeeder.Contracts.Providers
{
    using System.Collections.Generic;

    public interface ICommandNamesProvider
    {
        IEnumerable<string> CommandNames { get; }
    }
}
