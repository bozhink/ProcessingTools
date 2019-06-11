// <copyright file="PublishersWebProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Documents
{
    using global::AutoMapper;
    using ProcessingTools.Services.Models.Contracts.Documents.Publishers;
    using ProcessingTools.Web.Models.Documents.Publishers;

    /// <summary>
    /// Publishers (web) profile.
    /// </summary>
    public class PublishersWebProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublishersWebProfile"/> class.
        /// </summary>
        public PublishersWebProfile()
        {
            this.CreateMap<PublisherCreateRequestModel, PublisherCreateViewModel>();
            this.CreateMap<PublisherUpdateRequestModel, PublisherEditViewModel>();
            this.CreateMap<PublisherDeleteRequestModel, PublisherDeleteViewModel>();

            this.CreateMap<IPublisherModel, PublisherDeleteViewModel>();
            this.CreateMap<IPublisherModel, PublisherDetailsViewModel>();
            this.CreateMap<IPublisherModel, PublisherEditViewModel>();
            this.CreateMap<IPublisherModel, PublisherIndexViewModel>();
            this.CreateMap<IPublisherDetailsModel, PublisherDeleteViewModel>();
            this.CreateMap<IPublisherDetailsModel, PublisherDetailsViewModel>();
            this.CreateMap<IPublisherDetailsModel, PublisherEditViewModel>();
            this.CreateMap<IPublisherDetailsModel, PublisherIndexViewModel>();
        }
    }
}
