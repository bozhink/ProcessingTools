// <copyright file="PublishersDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Documents
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Documents.Publishers;
    using ProcessingTools.Data.Models.Mongo.Documents;

    /// <summary>
    /// Publishers data profile.
    /// </summary>
    public class PublishersDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublishersDataProfile"/> class.
        /// </summary>
        public PublishersDataProfile()
        {
            this.CreateMap<IPublisherInsertModel, Publisher>();
            this.CreateMap<IPublisherUpdateModel, Publisher>();
        }
    }
}
