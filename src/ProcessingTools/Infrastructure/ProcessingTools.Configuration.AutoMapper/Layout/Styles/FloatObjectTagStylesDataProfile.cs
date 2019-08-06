// <copyright file="FloatObjectTagStylesDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Layout.Styles
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;
    using ProcessingTools.Data.Models.Mongo.Layout;

    /// <summary>
    /// Float object tag styles data profile.
    /// </summary>
    public class FloatObjectTagStylesDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatObjectTagStylesDataProfile"/> class.
        /// </summary>
        public FloatObjectTagStylesDataProfile()
        {
            this.CreateMap<IFloatObjectInsertTagStyleModel, FloatObjectTagStyle>();
            this.CreateMap<IFloatObjectUpdateTagStyleModel, FloatObjectTagStyle>();
        }
    }
}
