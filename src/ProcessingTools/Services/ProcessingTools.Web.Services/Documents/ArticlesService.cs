// <copyright file="ArticlesService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Documents
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Models.Contracts.Documents.Articles;
    using ProcessingTools.Services.Models.Contracts.Documents.Documents;
    using ProcessingTools.Web.Models.Documents.Articles;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Articles service.
    /// </summary>
    public class ArticlesService : ProcessingTools.Web.Services.Contracts.Documents.IArticlesService
    {
        private readonly IArticlesService articlesService;
        private readonly Func<Task<UserContext>> userContextFactory;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesService"/> class.
        /// </summary>
        /// <param name="articlesService">Instance of <see cref="IArticlesService"/>.</param>
        /// <param name="userContext">User context.</param>
        public ArticlesService(IArticlesService articlesService, IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.articlesService = articlesService ?? throw new ArgumentNullException(nameof(articlesService));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<ArticleCreateRequestModel, ArticleCreateViewModel>();
                c.CreateMap<ArticleUpdateRequestModel, ArticleEditViewModel>();
                c.CreateMap<ArticleDeleteRequestModel, ArticleDeleteViewModel>();

                c.CreateMap<IArticleJournalModel, ArticleJournalViewModel>();
                c.CreateMap<IDocumentModel, ArticleDocumentViewModel>()
                    .ForMember(vm => vm.DocumentId, o => o.ResolveUsing(sm => sm.Id))
                    .ForMember(vm => vm.FileName, o => o.ResolveUsing(sm => sm.File?.FileName));

                c.CreateMap<IArticleModel, ArticleDeleteViewModel>();
                c.CreateMap<IArticleModel, ArticleDetailsViewModel>();
                c.CreateMap<IArticleModel, ArticleEditViewModel>();
                c.CreateMap<IArticleModel, ArticleIndexViewModel>();
                c.CreateMap<IArticleDetailsModel, ArticleDeleteViewModel>();
                c.CreateMap<IArticleDetailsModel, ArticleDetailsViewModel>();
                c.CreateMap<IArticleDetailsModel, ArticleEditViewModel>();
                c.CreateMap<IArticleDetailsModel, ArticleIndexViewModel>()
                    .ForMember(vm => vm.Journal, o => o.MapFrom(m => m.Journal));

                c.CreateMap<Microsoft.AspNetCore.Http.IFormFile, ArticleFileRequestModel>();
                c.CreateMap<Microsoft.AspNetCore.Http.IFormFile, IArticleFileModel>().As<ArticleFileRequestModel>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<bool> CreateArticleAsync(ArticleCreateRequestModel model)
        {
            if (model == null)
            {
                return false;
            }

            var result = await this.articlesService.CreateAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> CreateFromFileArticleAsync(Microsoft.AspNetCore.Http.IFormFile formFile, string journalId)
        {
            if (formFile == null)
            {
                throw new ArgumentNullException(nameof(formFile));
            }

            var model = this.mapper.Map<Microsoft.AspNetCore.Http.IFormFile, IArticleFileModel>(formFile);
            var result = await this.articlesService.CreateFromFileAsync(model, formFile.OpenReadStream(), journalId).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteArticleAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            var result = await this.articlesService.DeleteAsync(id).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> FinalizeArticleAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            var result = await this.articlesService.FinalizeAsync(id).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateArticleAsync(ArticleUpdateRequestModel model)
        {
            if (model == null)
            {
                return false;
            }

            var result = await this.articlesService.UpdateAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<ArticleCreateViewModel> GetArticleCreateViewModelAsync()
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            var journals = await this.GetArticleJournalsViewModelsAsync().ConfigureAwait(false);

            return new ArticleCreateViewModel(userContext, journals);
        }

        /// <inheritdoc/>
        public async Task<ArticleCreateFromFileViewModel> GetArticleCreateFromFileViewModelAsync()
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            var journals = await this.GetArticleJournalsViewModelsAsync().ConfigureAwait(false);

            return new ArticleCreateFromFileViewModel(userContext, journals);
        }

        /// <inheritdoc/>
        public async Task<ArticleDeleteViewModel> GetArticleDeleteViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var article = await this.articlesService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (article != null)
                {
                    var journal = this.mapper.Map<IArticleJournalModel, ArticleJournalViewModel>(article.Journal);

                    var viewModel = new ArticleDeleteViewModel(userContext, journal);
                    this.mapper.Map(article, viewModel);

                    return viewModel;
                }
            }

            return new ArticleDeleteViewModel(userContext, new ArticleJournalViewModel());
        }

        /// <inheritdoc/>
        public async Task<ArticleDetailsViewModel> GetArticleDetailsViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var article = await this.articlesService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (article != null)
                {
                    var journal = this.mapper.Map<IArticleJournalModel, ArticleJournalViewModel>(article.Journal);

                    var viewModel = new ArticleDetailsViewModel(userContext, journal);
                    this.mapper.Map(article, viewModel);

                    return viewModel;
                }
            }

            return new ArticleDetailsViewModel(userContext, new ArticleJournalViewModel());
        }

        /// <inheritdoc/>
        public async Task<ArticleDocumentsViewModel> GetArticleDocumentsViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var article = await this.articlesService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (article != null)
                {
                    var journal = this.mapper.Map<IArticleJournalModel, ArticleJournalViewModel>(article.Journal);

                    var documents = await this.articlesService.GetArticleDocumentsAsync(article.Id).ConfigureAwait(false);

                    var documentsViewModel = documents?.Select(this.mapper.Map<IDocumentModel, ArticleDocumentViewModel>).ToArray() ?? Array.Empty<ArticleDocumentViewModel>();

                    var viewModel = new ArticleDocumentsViewModel(userContext, journal, documentsViewModel);
                    this.mapper.Map(article, viewModel);

                    return viewModel;
                }
            }

            return new ArticleDocumentsViewModel(userContext, new ArticleJournalViewModel(), Array.Empty<ArticleDocumentViewModel>());
        }

        /// <inheritdoc/>
        public async Task<ArticleEditViewModel> GetArticleEditViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var article = await this.articlesService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (article != null)
                {
                    var journals = await this.GetArticleJournalsViewModelsAsync().ConfigureAwait(false);

                    var viewModel = new ArticleEditViewModel(userContext, journals);
                    this.mapper.Map(article, viewModel);

                    return viewModel;
                }
            }

            return new ArticleEditViewModel(userContext, Array.Empty<ArticleJournalViewModel>());
        }

        /// <inheritdoc/>
        public async Task<ArticlesIndexViewModel> GetArticlesIndexViewModelAsync(int skip, int take)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            var data = await this.articlesService.SelectDetailsAsync(skip, take).ConfigureAwait(false);
            var count = await this.articlesService.SelectCountAsync().ConfigureAwait(false);

            var articles = data?.Select(this.mapper.Map<IArticleDetailsModel, ArticleIndexViewModel>).ToArray() ?? Array.Empty<ArticleIndexViewModel>();

            return new ArticlesIndexViewModel(userContext, count, take, skip / take, articles);
        }

        /// <inheritdoc/>
        public async Task<ArticleCreateViewModel> MapToViewModelAsync(ArticleCreateRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);
            var journals = await this.GetArticleJournalsViewModelsAsync().ConfigureAwait(false);

            var viewModel = new ArticleCreateViewModel(userContext, journals);

            if (model != null)
            {
                this.mapper.Map(model, viewModel);
            }

            return viewModel;
        }

        /// <inheritdoc/>
        public async Task<ArticleEditViewModel> MapToViewModelAsync(ArticleUpdateRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var article = await this.articlesService.GetDetailsByIdAsync(model.Id).ConfigureAwait(false);
                if (article != null)
                {
                    var journals = await this.GetArticleJournalsViewModelsAsync().ConfigureAwait(false);

                    var viewModel = new ArticleEditViewModel(userContext, journals);
                    this.mapper.Map(model, viewModel);

                    viewModel.IsDeployed = article.IsDeployed;
                    viewModel.IsFinalized = article.IsFinalized;
                    viewModel.CreatedBy = article.CreatedBy;
                    viewModel.CreatedOn = article.CreatedOn;
                    viewModel.ModifiedBy = article.ModifiedBy;
                    viewModel.ModifiedOn = article.ModifiedOn;

                    return viewModel;
                }
            }

            return new ArticleEditViewModel(userContext, Array.Empty<ArticleJournalViewModel>());
        }

        /// <inheritdoc/>
        public async Task<ArticleDeleteViewModel> MapToViewModelAsync(ArticleDeleteRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var article = await this.articlesService.GetDetailsByIdAsync(model.Id).ConfigureAwait(false);
                if (article != null)
                {
                    var journal = this.mapper.Map<IArticleJournalModel, ArticleJournalViewModel>(article.Journal);

                    var viewModel = new ArticleDeleteViewModel(userContext, journal);
                    this.mapper.Map(article, viewModel);

                    return viewModel;
                }
            }

            return new ArticleDeleteViewModel(userContext, new ArticleJournalViewModel());
        }

        private async Task<ArticleJournalViewModel[]> GetArticleJournalsViewModelsAsync()
        {
            var articleJournals = await this.articlesService.GetArticleJournalsAsync().ConfigureAwait(false);

            return articleJournals?.Select(this.mapper.Map<IArticleJournalModel, ArticleJournalViewModel>).ToArray() ?? Array.Empty<ArticleJournalViewModel>();
        }
    }
}
