// <copyright file="ReferenceTagStylesWebProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Layout.Styles
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Services.Models.Layout.Styles.References;
    using ProcessingTools.Web.Models.Layout.Styles.References;

    /// <summary>
    /// Reference tag styles (web) profile.
    /// </summary>
    public class ReferenceTagStylesWebProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceTagStylesWebProfile"/> class.
        /// </summary>
        public ReferenceTagStylesWebProfile()
        {
            this.CreateMap<ReferenceTagStyleCreateRequestModel, ReferenceTagStyleCreateViewModel>();
            this.CreateMap<ReferenceTagStyleUpdateRequestModel, ReferenceTagStyleEditViewModel>();
            this.CreateMap<ReferenceTagStyleDeleteRequestModel, ReferenceTagStyleDeleteViewModel>();

            this.CreateMap<IReferenceTagStyleModel, ReferenceTagStyleDeleteViewModel>();
            this.CreateMap<IReferenceTagStyleModel, ReferenceTagStyleDetailsViewModel>();
            this.CreateMap<IReferenceTagStyleModel, ReferenceTagStyleEditViewModel>();
            this.CreateMap<IReferenceTagStyleModel, ReferenceTagStyleIndexViewModel>();
            this.CreateMap<IReferenceDetailsTagStyleModel, ReferenceTagStyleDeleteViewModel>();
            this.CreateMap<IReferenceDetailsTagStyleModel, ReferenceTagStyleDetailsViewModel>();
            this.CreateMap<IReferenceDetailsTagStyleModel, ReferenceTagStyleEditViewModel>();
            this.CreateMap<IReferenceDetailsTagStyleModel, ReferenceTagStyleIndexViewModel>();
        }
    }
}
