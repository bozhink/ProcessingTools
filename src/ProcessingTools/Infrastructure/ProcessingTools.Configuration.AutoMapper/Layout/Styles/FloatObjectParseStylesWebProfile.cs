// <copyright file="FloatObjectParseStylesWebProfile.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Layout.Styles
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;
    using ProcessingTools.Web.Models.Layout.Styles.Floats;

    /// <summary>
    /// Float object parse styles (web) profile.
    /// </summary>
    public class FloatObjectParseStylesWebProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatObjectParseStylesWebProfile"/> class.
        /// </summary>
        public FloatObjectParseStylesWebProfile()
        {
            this.CreateMap<FloatObjectParseStyleCreateRequestModel, FloatObjectParseStyleCreateViewModel>();
            this.CreateMap<FloatObjectParseStyleUpdateRequestModel, FloatObjectParseStyleEditViewModel>();
            this.CreateMap<FloatObjectParseStyleDeleteRequestModel, FloatObjectParseStyleDeleteViewModel>();

            this.CreateMap<IFloatObjectParseStyleModel, FloatObjectParseStyleDeleteViewModel>();
            this.CreateMap<IFloatObjectParseStyleModel, FloatObjectParseStyleDetailsViewModel>();
            this.CreateMap<IFloatObjectParseStyleModel, FloatObjectParseStyleEditViewModel>();
            this.CreateMap<IFloatObjectParseStyleModel, FloatObjectParseStyleIndexViewModel>();
            this.CreateMap<IFloatObjectDetailsParseStyleModel, FloatObjectParseStyleDeleteViewModel>();
            this.CreateMap<IFloatObjectDetailsParseStyleModel, FloatObjectParseStyleDetailsViewModel>();
            this.CreateMap<IFloatObjectDetailsParseStyleModel, FloatObjectParseStyleEditViewModel>();
            this.CreateMap<IFloatObjectDetailsParseStyleModel, FloatObjectParseStyleIndexViewModel>();
        }
    }
}
