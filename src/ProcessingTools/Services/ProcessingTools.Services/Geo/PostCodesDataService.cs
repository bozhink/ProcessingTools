// <copyright file="PostCodesDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;
    using ProcessingTools.Services.Contracts.Geo;

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
