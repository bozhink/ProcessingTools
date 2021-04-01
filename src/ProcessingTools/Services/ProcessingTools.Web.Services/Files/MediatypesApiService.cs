// <copyright file="MediatypesApiService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Files
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Contracts.Models.Files.Mediatypes;
    using ProcessingTools.Contracts.Services.Files;
    using ProcessingTools.Contracts.Web.Services.Files;
    using ProcessingTools.Web.Models.Resources.MediaTypes;

    /// <summary>
    /// Mediatypes API service.
    /// </summary>
    public class MediatypesApiService : IMediatypesApiService
    {
        private readonly IMediatypesResolver mediatypesResolver;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediatypesApiService"/> class.
        /// </summary>
        /// <param name="mediatypesResolver">Instance of <see cref="IMediatypesResolver"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public MediatypesApiService(IMediatypesResolver mediatypesResolver, IMapper mapper)
        {
            this.mediatypesResolver = mediatypesResolver ?? throw new ArgumentNullException(nameof(mediatypesResolver));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public async Task<IList<MediatypeResponseModel>> ResolveMediatypeAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return Array.Empty<MediatypeResponseModel>();
            }

            var result = await this.mediatypesResolver.ResolveMediatypeAsync(fileName).ConfigureAwait(false);
            if (result == null)
            {
                return Array.Empty<MediatypeResponseModel>();
            }

            return result.Select(this.mapper.Map<IMediatype, MediatypeResponseModel>).ToArray();
        }
    }
}
