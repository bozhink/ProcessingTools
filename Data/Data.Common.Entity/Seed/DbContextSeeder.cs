namespace ProcessingTools.Data.Common.Entity.Seed
{
    using System;
    using System.Collections.Concurrent;
    using System.Data.Entity;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using Contracts;

    public class DbContextSeeder<TContext>
        where TContext : DbContext
    {
        private const int NumberOfItemsToResetContext = 100;

        private readonly IDbContextProvider<TContext> contextProvider;

        public DbContextSeeder(IDbContextProvider<TContext> contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        /// <summary>
        /// Creates and imports simple one-line-data objects into an IDbContext from file with UTF8 encoding.
        /// </summary>
        /// <param name="fileName">Name of the file containing seed data.</param>
        /// <param name="createObject">Action to AddOrUpdate simple object to the IDbContext.</param>
        /// <returns>Number of successfully imported objects.</returns>
        /// <exception cref="FileNotFoundException">The file "fileName" is not found.</exception>
        /// <exception cref="AggregateException">Exceptions thrown by IDbContext.SaveChanges.</exception>
        public Task<int> ImportSingleLineTextObjectsFromFile(string fileName, Action<TContext, string> createObject)
        {
            return this.ImportSingleLineTextObjectsFromFile(fileName, createObject, Encoding.UTF8);
        }

        /// <summary>
        /// Creates and imports simple one-line-data objects into an IDbContext from file.
        /// </summary>
        /// <param name="fileName">Name of the file containing seed data.</param>
        /// <param name="createObject">Action to AddOrUpdate simple object to the IDbContext.</param>
        /// <param name="encoding">The encoding of the seed file.</param>
        /// <returns>Number of successfully imported objects.</returns>
        /// <exception cref="FileNotFoundException">The file "fileName" is not found.</exception>
        /// <exception cref="AggregateException">Exceptions thrown by IDbContext.SaveChanges.</exception>
        public async Task<int> ImportSingleLineTextObjectsFromFile(string fileName, Action<TContext, string> createObject, Encoding encoding)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("Missing seed file.", fileName);
            }

            int numberOfImportedObjects = 0;
            var exceptions = new ConcurrentQueue<Exception>();

            using (var stream = new StreamReader(fileName, encoding))
            {
                int localNumberOfImportedObjects = 0;

                var context = this.contextProvider.Create();

                string line = stream.ReadLine();
                for (int i = 0; line != null; ++i, line = stream.ReadLine())
                {
                    createObject(context, line);
                    ++localNumberOfImportedObjects;

                    if (i % NumberOfItemsToResetContext == 0)
                    {
                        try
                        {
                            await context.SaveChangesAsync();
                            numberOfImportedObjects += localNumberOfImportedObjects;
                            localNumberOfImportedObjects = 0;
                        }
                        catch (Exception e)
                        {
                            exceptions.Enqueue(e);
                        }

                        context.Dispose();
                        context = this.contextProvider.Create();
                    }
                }

                try
                {
                    await context.SaveChangesAsync();
                    numberOfImportedObjects += localNumberOfImportedObjects;
                }
                catch (Exception e)
                {
                    exceptions.Enqueue(e);
                }

                context.Dispose();
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException("Cannot import all data.", exceptions);
            }

            return numberOfImportedObjects;
        }
    }
}