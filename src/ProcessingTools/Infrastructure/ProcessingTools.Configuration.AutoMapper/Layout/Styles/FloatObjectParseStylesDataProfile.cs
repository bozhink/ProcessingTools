// <copyright file="FloatObjectParseStylesDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Layout.Styles
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;
    using ProcessingTools.Data.Models.Mongo.Layout;

    /// <summary>
    /// Float object parse styles data profile.
    /// </summary>
    public class FloatObjectParseStylesDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatObjectParseStylesDataProfile"/> class.
        /// </summary>
        public FloatObjectParseStylesDataProfile()
        {
            this.CreateMap<IFloatObjectInsertParseStyleModel, FloatObjectParseStyle>();
            this.CreateMap<IFloatObjectUpdateParseStyleModel, FloatObjectParseStyle>();
        }
    }
}
