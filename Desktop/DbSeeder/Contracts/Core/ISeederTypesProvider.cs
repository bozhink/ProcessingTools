namespace ProcessingTools.DbSeeder.Contracts.Core
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public interface ISeederTypesProvider
    {
        IEnumerable<Type> GetSeederTypes();

        IEnumerable<Type> GetSeederTypes<TSeeder>(params Assembly[] assemblies);
    }
}
