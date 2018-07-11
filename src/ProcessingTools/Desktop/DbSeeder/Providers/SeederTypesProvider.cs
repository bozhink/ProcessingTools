namespace ProcessingTools.DbSeeder.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

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
                            .Where(
                                t =>
                                    t.IsInterface &&
                                    !t.IsGenericType &&
                                    t.GetInterfaces().Any(i => i.FullName == this.baseName))
                            .ToArray();

                        if (types == null || types.Length < 1)
                        {
                            this.types = Array.Empty<Type>();
                        }
                        else
                        {
                            this.types = types;
                        }
                    }
                }
            }

            return this.types;
        }

        public Task<IEnumerable<Type>> GetTypesAsync()
        {
            return Task.Run(() => this.GetTypes());
        }
    }
}
