// <copyright file="DocumentsWebProfile.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Documents
{
    using global::AutoMapper;
    using Microsoft.AspNetCore.Http;
    using ProcessingTools.Contracts.Models.IO;
    using ProcessingTools.Contracts.Services.Models.Documents.Documents;
    using ProcessingTools.Web.Models.Documents.Documents;

    /// <summary>
    /// Documents (web) profile.
    /// </summary>
    public class DocumentsWebProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentsWebProfile"/> class.
        /// </summary>
        public DocumentsWebProfile()
        {
            this.CreateMap<IDocumentModel, DocumentEditViewModel>();
            this.CreateMap<IDocumentModel, DocumentDeleteViewModel>();
            this.CreateMap<IDocumentModel, DocumentDetailsViewModel>();
            this.CreateMap<IDocumentDetailsModel, DocumentEditViewModel>();
            this.CreateMap<IDocumentDetailsModel, DocumentDeleteViewModel>();
            this.CreateMap<IDocumentDetailsModel, DocumentDetailsViewModel>();

            this.CreateMap<IDocumentArticleModel, DocumentArticleViewModel>();
            this.CreateMap<IFileMetadata, DocumentFileViewModel>();

            this.CreateMap<IDocumentFileStreamModel, DocumentDownloadResponseModel>();

            this.CreateMap<IFormFile, DocumentFileRequestModel>()
                .ForMember(rm => rm.ContentLength, o => o.MapFrom(m => m.Length));
            this.CreateMap<IFormFile, IDocumentFileModel>().As<DocumentFileRequestModel>();
        }
    }
}
