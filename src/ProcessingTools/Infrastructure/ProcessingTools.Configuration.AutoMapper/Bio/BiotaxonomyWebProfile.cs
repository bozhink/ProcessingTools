// <copyright file="BiotaxonomyWebProfile.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Bio
{
    using global::AutoMapper;
    using ProcessingTools.Bio.Taxonomy.Api.Models;
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;
    using ProcessingTools.Bio.Taxonomy.Models;

    /// <summary>
    /// Bio taxonomy web profile.
    /// </summary>
    public class BiotaxonomyWebProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BiotaxonomyWebProfile"/> class.
        /// </summary>
        public BiotaxonomyWebProfile()
        {
            this.CreateMap<TaxonClassification, TaxonClassificationSearchResult>();
            this.CreateMap<ITaxonClassification, TaxonClassification>();
            this.CreateMap<ITaxonClassification, TaxonClassificationSearchResult>();
            this.CreateMap<ITaxonClassification, ITaxonClassificationSearchResult>().As<TaxonClassificationSearchResult>();

            this.CreateMap<TaxonRank, TaxonRankSearchResult>();
            this.CreateMap<ITaxonRank, TaxonRank>();
            this.CreateMap<ITaxonRank, TaxonRankSearchResult>();
            this.CreateMap<ITaxonRank, ITaxonRankSearchResult>().As<TaxonRankSearchResult>();

            this.CreateMap<ITaxonClassification, TaxonClassificationResponseModel>();
            this.CreateMap<ITaxonClassificationSearchResult, TaxonClassificationResponseModel>();
        }
    }
}
