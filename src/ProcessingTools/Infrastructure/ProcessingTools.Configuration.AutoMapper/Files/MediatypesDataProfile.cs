// <copyright file="MediatypesDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Files
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Files.Mediatypes;
    using ProcessingTools.Data.Models.Mongo.Files;

    /// <summary>
    /// Mediatypes data profile.
    /// </summary>
    public class MediatypesDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediatypesDataProfile"/> class.
        /// </summary>
        public MediatypesDataProfile()
        {
            this.CreateMap<IMediatypeInsertModel, Mediatype>();
            this.CreateMap<IMediatypeUpdateModel, Mediatype>();
        }
    }
}
