// <copyright file="FloatObjectTagStylesWebProfile.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Layout.Styles
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;
    using ProcessingTools.Web.Models.Layout.Styles.Floats;

    /// <summary>
    /// Float object tag styles (web) profile.
    /// </summary>
    public class FloatObjectTagStylesWebProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatObjectTagStylesWebProfile"/> class.
        /// </summary>
        public FloatObjectTagStylesWebProfile()
        {
            this.CreateMap<FloatObjectTagStyleCreateRequestModel, FloatObjectTagStyleCreateViewModel>();
            this.CreateMap<FloatObjectTagStyleUpdateRequestModel, FloatObjectTagStyleEditViewModel>();
            this.CreateMap<FloatObjectTagStyleDeleteRequestModel, FloatObjectTagStyleDeleteViewModel>();

            this.CreateMap<IFloatObjectTagStyleModel, FloatObjectTagStyleDeleteViewModel>();
            this.CreateMap<IFloatObjectTagStyleModel, FloatObjectTagStyleDetailsViewModel>();
            this.CreateMap<IFloatObjectTagStyleModel, FloatObjectTagStyleEditViewModel>();
            this.CreateMap<IFloatObjectTagStyleModel, FloatObjectTagStyleIndexViewModel>();
            this.CreateMap<IFloatObjectDetailsTagStyleModel, FloatObjectTagStyleDeleteViewModel>();
            this.CreateMap<IFloatObjectDetailsTagStyleModel, FloatObjectTagStyleDetailsViewModel>();
            this.CreateMap<IFloatObjectDetailsTagStyleModel, FloatObjectTagStyleEditViewModel>();
            this.CreateMap<IFloatObjectDetailsTagStyleModel, FloatObjectTagStyleIndexViewModel>();
        }
    }
}
