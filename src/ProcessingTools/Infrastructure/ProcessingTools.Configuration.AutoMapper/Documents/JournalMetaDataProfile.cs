// <copyright file="JournalMetaDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Documents
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Documents;
    using ProcessingTools.Data.Models.Mongo.Documents;

    /// <summary>
    /// Journal meta data profile.
    /// </summary>
    public class JournalMetaDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JournalMetaDataProfile"/> class.
        /// </summary>
        public JournalMetaDataProfile()
        {
            this.CreateMap<IJournalMeta, JournalMeta>();
        }
    }
}
