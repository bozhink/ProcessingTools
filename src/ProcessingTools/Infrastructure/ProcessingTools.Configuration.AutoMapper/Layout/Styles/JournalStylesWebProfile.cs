// <copyright file="JournalStylesWebProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Layout.Styles
{
    using System.Collections.Generic;
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles.Journals;
    using ProcessingTools.Web.Models.Layout.Styles.Journals;

    /// <summary>
    /// Journal styles (web) profile.
    /// </summary>
    public class JournalStylesWebProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JournalStylesWebProfile"/> class.
        /// </summary>
        public JournalStylesWebProfile()
        {
            this.CreateMap<JournalStyleCreateRequestModel, JournalStyleCreateViewModel>();
            this.CreateMap<JournalStyleUpdateRequestModel, JournalStyleEditViewModel>();
            this.CreateMap<JournalStyleDeleteRequestModel, JournalStyleDeleteViewModel>();

            this.CreateMap<IIdentifiedStyleModel, StyleSelectViewModel>();
            this.CreateMap<IFloatObjectDetailsParseStyleModel, StyleSelectViewModel>();
            this.CreateMap<IFloatObjectDetailsTagStyleModel, StyleSelectViewModel>();
            this.CreateMap<IReferenceDetailsParseStyleModel, StyleSelectViewModel>();
            this.CreateMap<IReferenceDetailsTagStyleModel, StyleSelectViewModel>();
            this.CreateMap<IList<IFloatObjectDetailsParseStyleModel>, IList<StyleSelectViewModel>>();
            this.CreateMap<IList<IFloatObjectDetailsTagStyleModel>, IList<StyleSelectViewModel>>();
            this.CreateMap<IList<IReferenceDetailsParseStyleModel>, IList<StyleSelectViewModel>>();
            this.CreateMap<IList<IReferenceDetailsTagStyleModel>, IList<StyleSelectViewModel>>();

            this.CreateMap<IJournalStyleModel, JournalStyleDeleteViewModel>();
            this.CreateMap<IJournalStyleModel, JournalStyleDetailsViewModel>();
            this.CreateMap<IJournalStyleModel, JournalStyleEditViewModel>();
            this.CreateMap<IJournalStyleModel, JournalStyleIndexViewModel>();
            this.CreateMap<IJournalDetailsStyleModel, JournalStyleDeleteViewModel>();
            this.CreateMap<IJournalDetailsStyleModel, JournalStyleDetailsViewModel>()
                .ForMember(vm => vm.FloatObjectParseStyles, o => o.MapFrom(sm => sm.FloatObjectParseStyles))
                .ForMember(vm => vm.FloatObjectTagStyles, o => o.MapFrom(sm => sm.FloatObjectTagStyles))
                .ForMember(vm => vm.ReferenceParseStyles, o => o.MapFrom(sm => sm.ReferenceParseStyles))
                .ForMember(vm => vm.ReferenceTagStyles, o => o.MapFrom(sm => sm.ReferenceTagStyles));
            this.CreateMap<IJournalDetailsStyleModel, JournalStyleEditViewModel>();
            this.CreateMap<IJournalDetailsStyleModel, JournalStyleIndexViewModel>();
        }
    }
}
