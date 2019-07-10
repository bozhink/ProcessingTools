// <copyright file="IDataRequester{T}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Generic data requester.
    /// </summary>
    /// <typeparam name="T">Type of returned object.</typeparam>
    public interface IDataRequester<T>
    {
        /// <summary>
        /// Requests related data to specified content.
        /// </summary>
        /// <param name="content">Search phrase.</param>
        /// <returns>Task of output type.</returns>
        Task<T> RequestDataAsync(string content);
    }
}
