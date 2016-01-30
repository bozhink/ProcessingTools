namespace ProcessingTools.Data.Common.Seed
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Text;

    using Contracts;

    public class DbContextSeeder<TContext>
        where TContext : IDbContext
    {
        private const int NumberOfItemsToResetContext = 100;

        /// <summary>
        /// Creates and imports simple one-line-data objects into an IDbContext from file with UTF8 encoding.
        /// </summary>
        /// <param name="fileName">Name of the file containing seed data.</param>
        /// <param name="createObject">Action to AddOrUpdate simple object to the IDbContext.</param>
        /// <returns>Number of successfully imported objects.</returns>
        /// <exception cref="FileNotFoundException">The file "fileName" is not found.</exception>
        /// <exception cref="AggregateException">Exceptions thrown by IDbContext.SaveChanges.</exception>
        public int ImportSingleLineTextObjectsFromFile(string fileName, Action<TContext, string> createObject)
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
        public int ImportSingleLineTextObjectsFromFile(string fileName, Action<TContext, string> createObject, Encoding encoding)
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

                var context = Activator.CreateInstance<TContext>();

                string line = stream.ReadLine();
                for (int i = 0; line != null; ++i, line = stream.ReadLine())
                {
                    createObject(context, line);
                    ++localNumberOfImportedObjects;

                    if (i % NumberOfItemsToResetContext == 0)
                    {
                        try
                        {
                            context.SaveChanges();
                            numberOfImportedObjects += localNumberOfImportedObjects;
                            localNumberOfImportedObjects = 0;
                        }
                        catch (Exception e)
                        {
                            exceptions.Enqueue(e);
                        }

                        context.Dispose();

                        context = Activator.CreateInstance<TContext>();
                    }
                }

                try
                {
                    context.SaveChanges();
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
