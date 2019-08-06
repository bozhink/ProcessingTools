// <copyright file="JournalsDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Documents
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Documents.Journals;
    using ProcessingTools.Data.Models.Mongo.Documents;

    /// <summary>
    /// Journals data profile.
    /// </summary>
    public class JournalsDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JournalsDataProfile"/> class.
        /// </summary>
        public JournalsDataProfile()
        {
            this.CreateMap<IJournalInsertModel, Journal>();
            this.CreateMap<IJournalUpdateModel, Journal>();
        }
    }
}
