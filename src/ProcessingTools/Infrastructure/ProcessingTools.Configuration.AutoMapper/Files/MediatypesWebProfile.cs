// <copyright file="MediatypesWebProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Files
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Services.Models.Files.Mediatypes;
    using ProcessingTools.Web.Models.Files.Mediatypes;

    /// <summary>
    /// Mediatypes (web) profile.
    /// </summary>
    public class MediatypesWebProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediatypesWebProfile"/> class.
        /// </summary>
        public MediatypesWebProfile()
        {
            this.CreateMap<MediatypeCreateRequestModel, MediatypeCreateViewModel>();
            this.CreateMap<MediatypeUpdateRequestModel, MediatypeEditViewModel>();
            this.CreateMap<MediatypeDeleteRequestModel, MediatypeDeleteViewModel>();

            this.CreateMap<IMediatypeModel, MediatypeDeleteViewModel>()
                .ForMember(vm => vm.ContentType, o => o.MapFrom(sm => sm.MimeType + "/" + sm.MimeSubtype));
            this.CreateMap<IMediatypeModel, MediatypeDetailsViewModel>()
                .ForMember(vm => vm.ContentType, o => o.MapFrom(sm => sm.MimeType + "/" + sm.MimeSubtype));
            this.CreateMap<IMediatypeModel, MediatypeEditViewModel>();
            this.CreateMap<IMediatypeModel, MediatypeIndexViewModel>()
                .ForMember(vm => vm.ContentType, o => o.MapFrom(sm => sm.MimeType + "/" + sm.MimeSubtype));
            this.CreateMap<IMediatypeDetailsModel, MediatypeDeleteViewModel>()
                .ForMember(vm => vm.ContentType, o => o.MapFrom(sm => sm.MimeType + "/" + sm.MimeSubtype));
            this.CreateMap<IMediatypeDetailsModel, MediatypeDetailsViewModel>()
                .ForMember(vm => vm.ContentType, o => o.MapFrom(sm => sm.MimeType + "/" + sm.MimeSubtype));
            this.CreateMap<IMediatypeDetailsModel, MediatypeEditViewModel>();
            this.CreateMap<IMediatypeDetailsModel, MediatypeIndexViewModel>()
                .ForMember(vm => vm.ContentType, o => o.MapFrom(sm => sm.MimeType + "/" + sm.MimeSubtype));
        }
    }
}
