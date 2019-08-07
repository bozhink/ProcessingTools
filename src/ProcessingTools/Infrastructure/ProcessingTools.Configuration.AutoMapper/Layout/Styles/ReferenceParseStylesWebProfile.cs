// <copyright file="ReferenceParseStylesWebProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Layout.Styles
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;
    using ProcessingTools.Web.Models.Layout.Styles.References;

    /// <summary>
    /// Reference parse styles (web) profile.
    /// </summary>
    public class ReferenceParseStylesWebProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceParseStylesWebProfile"/> class.
        /// </summary>
        public ReferenceParseStylesWebProfile()
        {
            this.CreateMap<ReferenceParseStyleCreateRequestModel, ReferenceParseStyleCreateViewModel>();
            this.CreateMap<ReferenceParseStyleUpdateRequestModel, ReferenceParseStyleEditViewModel>();
            this.CreateMap<ReferenceParseStyleDeleteRequestModel, ReferenceParseStyleDeleteViewModel>();

            this.CreateMap<IReferenceParseStyleModel, ReferenceParseStyleDeleteViewModel>();
            this.CreateMap<IReferenceParseStyleModel, ReferenceParseStyleDetailsViewModel>();
            this.CreateMap<IReferenceParseStyleModel, ReferenceParseStyleEditViewModel>();
            this.CreateMap<IReferenceParseStyleModel, ReferenceParseStyleIndexViewModel>();
            this.CreateMap<IReferenceDetailsParseStyleModel, ReferenceParseStyleDeleteViewModel>();
            this.CreateMap<IReferenceDetailsParseStyleModel, ReferenceParseStyleDetailsViewModel>();
            this.CreateMap<IReferenceDetailsParseStyleModel, ReferenceParseStyleEditViewModel>();
            this.CreateMap<IReferenceDetailsParseStyleModel, ReferenceParseStyleIndexViewModel>();
        }
    }
}
