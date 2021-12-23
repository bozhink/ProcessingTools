// <copyright file="DocumentsDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Documents
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Documents.Documents;
    using ProcessingTools.Data.Models.Mongo.Documents;

    /// <summary>
    /// Documents data profile.
    /// </summary>
    public class DocumentsDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentsDataProfile"/> class.
        /// </summary>
        public DocumentsDataProfile()
        {
            this.CreateMap<IDocumentFileModel, File>();
            this.CreateMap<IDocumentInsertModel, Document>();
            this.CreateMap<IDocumentUpdateModel, Document>();
        }
    }
}
