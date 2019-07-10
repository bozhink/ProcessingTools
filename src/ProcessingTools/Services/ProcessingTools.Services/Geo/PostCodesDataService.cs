// <copyright file="PostCodesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Geo;

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;

    /// <summary>
    /// Post codes data service.
    /// </summary>
    public class PostCodesDataService : AbstractGeoDataService<IPostCodesRepository, IPostCode, IPostCodesFilter>, IPostCodesDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostCodesDataService"/> class.
        /// </summary>
        /// <param name="repository">Instance of <see cref="IPostCodesRepository"/>.</param>
        public PostCodesDataService(IPostCodesRepository repository)
            : base(repository)
        {
        }
    }
}
