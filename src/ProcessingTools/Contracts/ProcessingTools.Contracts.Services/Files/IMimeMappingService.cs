// <copyright file="IMimeMappingService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Files
{
    using System.Threading.Tasks;

    /// <summary>
    /// MIME mapping service.
    /// </summary>
    /// <remarks>
    /// See https://dotnetcoretutorials.com/2018/08/14/getting-a-mime-type-from-a-file-name-in-net-core/.
    /// </remarks>
    public interface IMimeMappingService
    {
        /// <summary>
        /// Maps file name to MIME type.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <returns>MIME type.</returns>
        Task<string> MapAsync(string fileName);
    }
}
