// <copyright file="ArticlesWebProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Documents
{
    using global::AutoMapper;
    using ProcessingTools.Services.Models.Contracts.Documents.Articles;
    using ProcessingTools.Services.Models.Contracts.Documents.Documents;
    using ProcessingTools.Web.Models.Documents.Articles;

    /// <summary>
    /// Articles (web) profile.
    /// </summary>
    public class ArticlesWebProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesWebProfile"/> class.
        /// </summary>
        public ArticlesWebProfile()
        {
            this.CreateMap<ArticleCreateRequestModel, ArticleCreateViewModel>();
            this.CreateMap<ArticleUpdateRequestModel, ArticleEditViewModel>();
            this.CreateMap<ArticleDeleteRequestModel, ArticleDeleteViewModel>();

            this.CreateMap<IArticleJournalModel, ArticleJournalViewModel>();
            this.CreateMap<IDocumentModel, ArticleDocumentViewModel>()
                .ForMember(vm => vm.DocumentId, o => o.MapFrom(sm => sm.Id))
                .ForMember(vm => vm.FileName, o => o.MapFrom(sm => sm.File != null ? sm.File.FileName : null));

            this.CreateMap<IArticleModel, ArticleDeleteViewModel>();
            this.CreateMap<IArticleModel, ArticleDetailsViewModel>();
            this.CreateMap<IArticleModel, ArticleEditViewModel>();
            this.CreateMap<IArticleModel, ArticleIndexViewModel>();
            this.CreateMap<IArticleDetailsModel, ArticleDeleteViewModel>();
            this.CreateMap<IArticleDetailsModel, ArticleDetailsViewModel>();
            this.CreateMap<IArticleDetailsModel, ArticleEditViewModel>();
            this.CreateMap<IArticleDetailsModel, ArticleIndexViewModel>()
                .ForMember(vm => vm.Journal, o => o.MapFrom(m => m.Journal));

            this.CreateMap<Microsoft.AspNetCore.Http.IFormFile, ArticleFileRequestModel>();
            this.CreateMap<Microsoft.AspNetCore.Http.IFormFile, IArticleFileModel>().As<ArticleFileRequestModel>();
        }
    }
}