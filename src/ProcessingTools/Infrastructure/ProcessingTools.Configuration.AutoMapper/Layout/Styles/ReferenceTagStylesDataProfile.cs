// <copyright file="ReferenceTagStylesDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Layout.Styles
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;
    using ProcessingTools.Data.Models.Mongo.Layout;

    /// <summary>
    /// Reference tag styles data profile.
    /// </summary>
    public class ReferenceTagStylesDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceTagStylesDataProfile"/> class.
        /// </summary>
        public ReferenceTagStylesDataProfile()
        {
            this.CreateMap<IReferenceInsertTagStyleModel, ReferenceTagStyle>();
            this.CreateMap<IReferenceUpdateTagStyleModel, ReferenceTagStyle>();
        }
    }
}
