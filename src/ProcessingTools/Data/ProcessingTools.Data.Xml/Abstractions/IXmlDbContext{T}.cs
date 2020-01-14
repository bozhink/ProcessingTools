// <copyright file="IXmlDbContext{T}.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Xml.Abstractions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// File DB context.
    /// </summary>
    /// <typeparam name="T">Type of file entity.</typeparam>
    public interface IXmlDbContext<T>
    {
        /// <summary>
        /// Gets the data set.
        /// </summary>
        IEnumerable<T> DataSet { get; }

        /// <summary>
        /// Updates or inserts entity into the DB context.
        /// </summary>
        /// <param name="entity">Entity to be inserted.</param>
        /// <returns>Result.</returns>
        object Upsert(T entity);

        /// <summary>
        /// Deletes entity from the DB context.
        /// </summary>
        /// <param name="id">ID of the entity to be deleted.</param>
        /// <returns>Result.</returns>
        object Delete(object id);

        /// <summary>
        /// Gets entity by ID.
        /// </summary>
        /// <param name="id">ID of the entity.</param>
        /// <returns>Resultant entity.</returns>
        T Get(object id);

        /// <summary>
        /// Loads data into the DB context from file.
        /// </summary>
        /// <param name="fileName">Name of the file to be loaded.</param>
        /// <returns>Number of loaded entities.</returns>
        long LoadFromFile(string fileName);

        /// <summary>
        /// Writes entities from the context to file.
        /// </summary>
        /// <param name="fileName">Name of the file to be written.</param>
        /// <returns>Number of written entities.</returns>
        Task<long> WriteToFileAsync(string fileName);
    }
}
