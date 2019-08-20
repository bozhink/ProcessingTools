// <copyright file="BiotaxonomyWebProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Bio
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Web.Models.Bio.Taxonomy;

    /// <summary>
    /// Biotaxonomy web profile.
    /// </summary>
    public class BiotaxonomyWebProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BiotaxonomyWebProfile"/> class.
        /// </summary>
        public BiotaxonomyWebProfile()
        {
            this.CreateMap<ITaxonClassification, TaxonClassificationResponseModel>();
        }
    }
}
