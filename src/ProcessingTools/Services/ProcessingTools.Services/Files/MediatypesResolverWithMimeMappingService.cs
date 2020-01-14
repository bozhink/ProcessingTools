// <copyright file="MediatypesResolverWithMimeMappingService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Files
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.Models.Files.Mediatypes;
    using ProcessingTools.Contracts.Services.Files;
    using ProcessingTools.Services.Models.Data.Mediatypes;

    /// <summary>
    /// Mediatypes resolver with MIME mapping service.
    /// </summary>
    public class MediatypesResolverWithMimeMappingService : IMediatypesResolver
    {
        private readonly IMimeMappingService mimeMappingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediatypesResolverWithMimeMappingService"/> class.
        /// </summary>
        /// <param name="mimeMappingService">MIME mapping service.</param>
        public MediatypesResolverWithMimeMappingService(IMimeMappingService mimeMappingService)
        {
            this.mimeMappingService = mimeMappingService ?? throw new ArgumentNullException(nameof(mimeMappingService));
        }

        /// <inheritdoc/>
        public async Task<IList<IMediatype>> ResolveMediatypeAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new FileNameIsNullOrWhitespaceException();
            }

            string mediatype = await this.mimeMappingService.MapAsync(fileName).ConfigureAwait(false);

            return new[]
            {
                new Mediatype(mediatype),
            };
        }
    }
}
