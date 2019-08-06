// <copyright file="ReferenceParseStylesDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Layout.Styles
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;
    using ProcessingTools.Data.Models.Mongo.Layout;

    /// <summary>
    /// Reference parse styles data profile.
    /// </summary>
    public class ReferenceParseStylesDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceParseStylesDataProfile"/> class.
        /// </summary>
        public ReferenceParseStylesDataProfile()
        {
            this.CreateMap<IReferenceInsertParseStyleModel, ReferenceParseStyle>();
            this.CreateMap<IReferenceUpdateParseStyleModel, ReferenceParseStyle>();
        }
    }
}
