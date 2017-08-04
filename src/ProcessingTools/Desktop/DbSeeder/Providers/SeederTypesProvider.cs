namespace ProcessingTools.DbSeeder.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Contracts.Seeders;
    using ProcessingTools.Contracts;

    internal class SeederTypesProvider : ITypesProvider
    {
        private readonly string baseName = typeof(IDbSeeder).FullName;
        private readonly object lockKey = new object();
        private IEnumerable<Type> types;

        public IEnumerable<Type> GetTypes()
        {
            if (this.types == null)
            {
                lock (this.lockKey)
                {
                    if (this.types == null)
                    {
                        var assembly = Assembly.GetExecutingAssembly();
                        var types = assembly.GetTypes()
                            .Where(t => t.IsInterface &&
                                        !t.IsGenericType &&
                                        t.GetInterfaces()
                                            .Any(i => i.FullName == this.baseName))
                            .ToArray();

                        if (types == null || types.Length < 1)
                        {
                            throw new ApplicationException("No seeders are found");
                        }

                        this.types = types;
                    }
                }
            }

            return this.types;
        }
    }
}
