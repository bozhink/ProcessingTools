// <copyright file="XmlDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Xml.Abstractions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Abstract XML data access object (DAO).
    /// </summary>
    /// <typeparam name="TContext">Type of the XML DB context.</typeparam>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    public abstract class XmlDataAccessObject<TContext, TEntity>
        where TContext : class, IXmlDbContext<TEntity>
        where TEntity : class
    {
        private readonly string dataFileName;
        private readonly Lazy<TContext> contextLazy;
        private readonly object syncRoot = new object();
        private bool isContentLoaded = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlDataAccessObject{TContext, TEntity}"/> class.
        /// </summary>
        /// <param name="dataFileName">File name of the data XML file.</param>
        /// <param name="context">XML context to be requested.</param>
        protected XmlDataAccessObject(string dataFileName, TContext context)
        {
            if (string.IsNullOrWhiteSpace(dataFileName))
            {
                throw new ArgumentNullException(nameof(dataFileName));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.dataFileName = dataFileName;
            this.contextLazy = new Lazy<TContext>(() =>
            {
                if (!this.isContentLoaded && !context.DataSet.Any())
                {
                    lock (this.syncRoot)
                    {
                        if (!this.isContentLoaded && !context.DataSet.Any())
                        {
                            context.LoadFromFile(this.dataFileName);
                            this.isContentLoaded = true;
                        }
                    }
                }

                return context;
            });
        }

        /// <summary>
        /// Gets the configured XML context.
        /// </summary>
        protected TContext Context => this.contextLazy.Value;

        /// <summary>
        /// Writes entities from the context to file.
        /// </summary>
        /// <returns>Number of written entities.</returns>
        public virtual Task<long> SaveChangesAsync() => this.Context.WriteToFileAsync(this.dataFileName);
    }
}
