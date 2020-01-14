// <copyright file="MediatypesResolverWithDatabase.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Files
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.DataAccess.Files;
    using ProcessingTools.Contracts.Models.Files.Mediatypes;
    using ProcessingTools.Contracts.Services.Files;
    using ProcessingTools.Services.Models.Data.Mediatypes;

    /// <summary>
    /// Mediatypes resolver with database.
    /// </summary>
    public class MediatypesResolverWithDatabase : IMediatypesResolver
    {
        private readonly IMediatypesDataAccessObject dataAccessObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediatypesResolverWithDatabase"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Data access object.</param>
        public MediatypesResolverWithDatabase(IMediatypesDataAccessObject dataAccessObject)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
        }

        /// <inheritdoc/>
        public async Task<IList<IMediatype>> ResolveMediatypeAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new FileNameIsNullOrWhitespaceException();
            }

            string extension = Path.GetExtension(fileName).Trim('.', ' ', '\n', '\r');

            try
            {
                var response = await this.dataAccessObject.GetMediatypesByExtensionAsync(extension).ConfigureAwait(false);

                if (response == null || !response.Any())
                {
                    return this.GetDefaultResult();
                }
                else
                {
                    return response.Select(e => new Mediatype(e.MimeType, e.MimeSubtype)).ToArray();
                }
            }
            catch
            {
                return this.GetDefaultResult();
            }
        }

        private IList<IMediatype> GetDefaultResult()
        {
            return new[]
            {
                new Mediatype(ContentTypes.DefaultMimeType, ContentTypes.DefaultMimeSubtype),
            };
        }
    }
}
