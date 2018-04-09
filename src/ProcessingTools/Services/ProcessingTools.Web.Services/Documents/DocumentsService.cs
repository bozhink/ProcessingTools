﻿// <copyright file="DocumentsService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Documents
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using ProcessingTools.Contracts;
    using ProcessingTools.Models.Contracts.IO;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Models.Contracts.Documents.Documents;
    using ProcessingTools.Web.Models.Documents.Documents;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Documents service.
    /// </summary>
    public class DocumentsService : ProcessingTools.Web.Services.Contracts.Documents.IDocumentsService
    {
        private readonly IDocumentsService documentsService;
        private readonly Func<Task<UserContext>> userContextFactory;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentsService"/> class.
        /// </summary>
        /// <param name="documentsService">Instance of <see cref="IDocumentsService"/>.</param>
        /// <param name="userContext">User context.</param>
        public DocumentsService(IDocumentsService documentsService, IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.documentsService = documentsService ?? throw new ArgumentNullException(nameof(documentsService));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IDocumentModel, DocumentEditViewModel>();
                c.CreateMap<IDocumentModel, DocumentDeleteViewModel>();
                c.CreateMap<IDocumentModel, DocumentDetailsViewModel>();
                c.CreateMap<IDocumentDetailsModel, DocumentEditViewModel>();
                c.CreateMap<IDocumentDetailsModel, DocumentDeleteViewModel>();
                c.CreateMap<IDocumentDetailsModel, DocumentDetailsViewModel>();

                c.CreateMap<IDocumentArticle, DocumentArticleViewModel>();
                c.CreateMap<IFileMetadata, DocumentFileViewModel>();

                c.CreateMap<IDocumentFileStreamModel, DocumentDownloadResponseModel>();

                c.CreateMap<IFormFile, DocumentFileRequestModel>()
                    .ForMember(rm => rm.ContentLength, o => o.ResolveUsing(m => m.Length));
                c.CreateMap<IFormFile, IDocumentFileModel>().As<DocumentFileRequestModel>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<DocumentUploadViewModel> GetDocumentUploadViewModelAsync(string articleId)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(articleId))
            {
                var article = await this.documentsService.GetDocumentArticleAsync(articleId).ConfigureAwait(false);
                if (article != null)
                {
                    var articleViewModel = this.mapper.Map<IDocumentArticle, DocumentArticleViewModel>(article);

                    return new DocumentUploadViewModel(userContext, articleViewModel);
                }
            }

            return new DocumentUploadViewModel(userContext, new DocumentArticleViewModel());
        }

        /// <inheritdoc/>
        public async Task<DocumentEditViewModel> GetDocumentEditViewModelAsync(string id, string articleId)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(articleId))
            {
                var article = await this.documentsService.GetDocumentArticleAsync(articleId).ConfigureAwait(false);
                if (article != null)
                {
                    var document = await this.documentsService.GetDetailsByIdAsync(id, articleId).ConfigureAwait(false);
                    if (document != null)
                    {
                        var articleViewModel = this.mapper.Map<IDocumentArticle, DocumentArticleViewModel>(article);

                        var fileViewModel = this.mapper.Map<IFileMetadata, DocumentFileViewModel>(document.File);
                        fileViewModel.FileId = document.FileId;

                        var documentViewModel = new DocumentEditViewModel(userContext, articleViewModel, fileViewModel);
                        this.mapper.Map<IDocumentDetailsModel, DocumentEditViewModel>(document, documentViewModel);

                        return documentViewModel;
                    }
                }
            }

            return new DocumentEditViewModel(userContext, new DocumentArticleViewModel(), new DocumentFileViewModel());
        }

        /// <inheritdoc/>
        public async Task<DocumentDeleteViewModel> GetDocumentDeleteViewModelAsync(string id, string articleId)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(articleId))
            {
                var article = await this.documentsService.GetDocumentArticleAsync(articleId).ConfigureAwait(false);
                if (article != null)
                {
                    var document = await this.documentsService.GetDetailsByIdAsync(id, articleId).ConfigureAwait(false);
                    if (document != null)
                    {
                        var articleViewModel = this.mapper.Map<IDocumentArticle, DocumentArticleViewModel>(article);

                        var fileViewModel = this.mapper.Map<IFileMetadata, DocumentFileViewModel>(document.File);
                        fileViewModel.FileId = document.FileId;

                        var documentViewModel = new DocumentDeleteViewModel(userContext, articleViewModel, fileViewModel);
                        this.mapper.Map<IDocumentDetailsModel, DocumentDeleteViewModel>(document, documentViewModel);

                        return documentViewModel;
                    }
                }
            }

            return new DocumentDeleteViewModel(userContext, new DocumentArticleViewModel(), new DocumentFileViewModel());
        }

        /// <inheritdoc/>
        public async Task<DocumentDetailsViewModel> GetDocumentDetailsViewModelAsync(string id, string articleId)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(articleId))
            {
                var article = await this.documentsService.GetDocumentArticleAsync(articleId).ConfigureAwait(false);
                if (article != null)
                {
                    var document = await this.documentsService.GetDetailsByIdAsync(id, articleId).ConfigureAwait(false);
                    if (document != null)
                    {
                        var articleViewModel = this.mapper.Map<IDocumentArticle, DocumentArticleViewModel>(article);

                        var fileViewModel = this.mapper.Map<IFileMetadata, DocumentFileViewModel>(document.File);
                        fileViewModel.FileId = document.FileId;

                        var documentViewModel = new DocumentDetailsViewModel(userContext, articleViewModel, fileViewModel);
                        this.mapper.Map<IDocumentDetailsModel, DocumentDetailsViewModel>(document, documentViewModel);

                        return documentViewModel;
                    }
                }
            }

            return new DocumentDetailsViewModel(userContext, new DocumentArticleViewModel(), new DocumentFileViewModel());
        }

        /// <inheritdoc/>
        public async Task<DocumentDownloadResponseModel> DownloadDocumentAsync(string id, string articleId)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(articleId))
            {
                return null;
            }

            var model = await this.documentsService.DownloadAsync(id, articleId).ConfigureAwait(false);
            if (model == null)
            {
                return null;
            }

            var response = this.mapper.Map<IDocumentFileStreamModel, DocumentDownloadResponseModel>(model);
            return response;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteDocumentAsync(string id, string articleId)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(articleId))
            {
                return false;
            }

            var result = await this.documentsService.DeleteAsync(id, articleId).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateDocumentAsync(DocumentUpdateRequestModel model)
        {
            if (model == null)
            {
                return false;
            }

            var result = await this.documentsService.UpdateAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> UploadDocumentAsync(IFormFile formFile, string articleId)
        {
            if (formFile == null || string.IsNullOrWhiteSpace(articleId))
            {
                return false;
            }

            var model = this.mapper.Map<IFormFile, IDocumentFileModel>(formFile);

            var result = await this.documentsService.UploadAsync(model, formFile.OpenReadStream(), articleId).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> SetAsFinalAsync(string id, string articleId)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(articleId))
            {
                return false;
            }

            var result = await this.documentsService.SetAsFinalAsync(id, articleId).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<string> GetHtmlAsync(string id, string articleId)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(articleId))
            {
                return null;
            }

            string content = await this.documentsService.GetHtmlAsync(id, articleId).ConfigureAwait(false);
            return content;
        }

        /// <inheritdoc/>
        public async Task<string> GetXmlAsync(string id, string articleId)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(articleId))
            {
                return null;
            }

            string content = await this.documentsService.GetXmlAsync(id, articleId).ConfigureAwait(false);
            return content;
        }

        /// <inheritdoc/>
        public async Task<bool> SetHtmlAsync(string id, string articleId, string content)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(articleId))
            {
                return false;
            }

            var result = await this.documentsService.SetHtmlAsync(id, articleId, content).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> SetXmlAsync(string id, string articleId, string content)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(articleId))
            {
                return false;
            }

            var result = await this.documentsService.SetXmlAsync(id, articleId, content).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<DocumentEditViewModel> MapToViewModelAsync(DocumentUpdateRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id) && !string.IsNullOrWhiteSpace(model.ArticleId))
            {
                var article = await this.documentsService.GetDocumentArticleAsync(model.ArticleId).ConfigureAwait(false);
                if (article != null)
                {
                    var document = await this.documentsService.GetDetailsByIdAsync(model.Id, model.ArticleId).ConfigureAwait(false);
                    if (document != null)
                    {
                        var articleViewModel = this.mapper.Map<IDocumentArticle, DocumentArticleViewModel>(article);

                        var fileViewModel = this.mapper.Map<IFileMetadata, DocumentFileViewModel>(document.File);
                        fileViewModel.FileId = document.FileId;

                        var documentViewModel = new DocumentEditViewModel(userContext, articleViewModel, fileViewModel);
                        this.mapper.Map<IDocumentDetailsModel, DocumentEditViewModel>(document, documentViewModel);

                        documentViewModel.Description = model.Description;
                        documentViewModel.File.FileName = model.FileName;

                        return documentViewModel;
                    }
                }
            }

            return new DocumentEditViewModel(userContext, new DocumentArticleViewModel(), new DocumentFileViewModel());
        }

        /// <inheritdoc/>
        public async Task<DocumentDeleteViewModel> MapToViewModelAsync(DocumentDeleteRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id) && !string.IsNullOrWhiteSpace(model.ArticleId))
            {
                return await this.GetDocumentDeleteViewModelAsync(model.Id, model.ArticleId).ConfigureAwait(false);
            }

            return new DocumentDeleteViewModel(userContext, new DocumentArticleViewModel(), new DocumentFileViewModel());
        }
    }
}
