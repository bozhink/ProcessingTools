// <copyright file="JournalsWebProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Documents
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Layout.Styles;
    using ProcessingTools.Contracts.Services.Models.Documents.Journals;
    using ProcessingTools.Web.Models.Documents.Journals;

    /// <summary>
    /// Journals (web) profile.
    /// </summary>
    public class JournalsWebProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JournalsWebProfile"/> class.
        /// </summary>
        public JournalsWebProfile()
        {
            this.CreateMap<JournalCreateRequestModel, JournalCreateViewModel>();
            this.CreateMap<JournalUpdateRequestModel, JournalEditViewModel>();
            this.CreateMap<JournalDeleteRequestModel, JournalDeleteViewModel>();

            this.CreateMap<IJournalPublisherModel, JournalPublisherViewModel>();
            this.CreateMap<IIdentifiedStyleModel, JournalStyleViewModel>();

            this.CreateMap<IJournalModel, JournalDeleteViewModel>();
            this.CreateMap<IJournalModel, JournalDetailsViewModel>();
            this.CreateMap<IJournalModel, JournalEditViewModel>();
            this.CreateMap<IJournalModel, JournalIndexViewModel>();
            this.CreateMap<IJournalDetailsModel, JournalDeleteViewModel>();
            this.CreateMap<IJournalDetailsModel, JournalDetailsViewModel>();
            this.CreateMap<IJournalDetailsModel, JournalEditViewModel>();
            this.CreateMap<IJournalDetailsModel, JournalIndexViewModel>()
                .ForMember(vm => vm.Publisher, o => o.MapFrom(m => m.Publisher));
        }
    }
}
