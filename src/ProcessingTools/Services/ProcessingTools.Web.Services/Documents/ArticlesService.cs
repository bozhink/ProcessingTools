// <copyright file="ArticlesService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
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
    using ProcessingTools.Web.Models.Documents.Articles;
    using ProcessingTools.Web.Models.Shared;
    using ProcessingTools.Web.Services.Contracts.Documents;

    /// <summary>
    /// Articles service.
    /// </summary>
    public class ArticlesService : IArticlesService
    {
        private readonly IArticlesDataService articlesDataService;
        private readonly Func<Task<UserContext>> userContextFactory;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesService"/> class.
        /// </summary>
        /// <param name="articlesDataService">Instance of <see cref="IArticlesDataService"/>.</param>
        /// <param name="userContext">User context.</param>
        public ArticlesService(IArticlesDataService articlesDataService, IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.articlesDataService = articlesDataService ?? throw new ArgumentNullException(nameof(articlesDataService));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<ArticleCreateRequestModel, ArticleCreateViewModel>();
                c.CreateMap<ArticleUpdateRequestModel, ArticleEditViewModel>();
                c.CreateMap<ArticleDeleteRequestModel, ArticleDeleteViewModel>();

                c.CreateMap<IArticleJournalModel, ArticleJournalViewModel>();

                c.CreateMap<IArticleDetailsModel, ArticleDeleteViewModel>();
                c.CreateMap<IArticleDetailsModel, ArticleDetailsViewModel>();
                c.CreateMap<IArticleDetailsModel, ArticleEditViewModel>();
                c.CreateMap<IArticleDetailsModel, ArticleIndexViewModel>()
                    .ForMember(vm => vm.Journal, o => o.MapFrom(m => m.Journal));
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

            var result = await this.articlesDataService.InsertAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteArticleAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            var result = await this.articlesDataService.DeleteAsync(id).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateArticleAsync(ArticleUpdateRequestModel model)
        {
            if (model == null)
            {
                return false;
            }

            var result = await this.articlesDataService.UpdateAsync(model).ConfigureAwait(false);
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
        public async Task<ArticleDeleteViewModel> GetArticleDeleteViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var article = await this.articlesDataService.GetDetailsById(id).ConfigureAwait(false);
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
                var article = await this.articlesDataService.GetDetailsById(id).ConfigureAwait(false);
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
        public async Task<ArticleEditViewModel> GetArticleEditViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var article = await this.articlesDataService.GetDetailsById(id).ConfigureAwait(false);
                if (article != null)
                {
                    var journals = await this.GetArticleJournalsViewModelsAsync().ConfigureAwait(false);

                    var viewModel = new ArticleEditViewModel(userContext, journals);
                    this.mapper.Map(article, viewModel);

                    return viewModel;
                }
            }

            return new ArticleEditViewModel(userContext, new ArticleJournalViewModel[] { });
        }

        /// <inheritdoc/>
        public async Task<ArticlesIndexViewModel> GetArticlesIndexViewModelAsync(int skip, int take)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            var data = await this.articlesDataService.SelectDetailsAsync(skip, take).ConfigureAwait(false);
            var count = await this.articlesDataService.SelectCountAsync().ConfigureAwait(false);

            var articles = data?.Select(this.mapper.Map<IArticleDetailsModel, ArticleIndexViewModel>).ToArray() ?? new ArticleIndexViewModel[] { };

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

            viewModel.ReturnUrl = model?.ReturnUrl;

            return viewModel;
        }

        /// <inheritdoc/>
        public async Task<ArticleEditViewModel> MapToViewModelAsync(ArticleUpdateRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var article = await this.articlesDataService.GetDetailsById(model.Id).ConfigureAwait(false);
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

            return new ArticleEditViewModel(userContext, new ArticleJournalViewModel[] { })
            {
                ReturnUrl = model?.ReturnUrl
            };
        }

        /// <inheritdoc/>
        public async Task<ArticleDeleteViewModel> MapToViewModelAsync(ArticleDeleteRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var article = await this.articlesDataService.GetDetailsById(model.Id).ConfigureAwait(false);
                if (article != null)
                {
                    var journal = this.mapper.Map<IArticleJournalModel, ArticleJournalViewModel>(article.Journal);

                    var viewModel = new ArticleDeleteViewModel(userContext, journal);
                    this.mapper.Map(article, viewModel);

                    viewModel.ReturnUrl = model.ReturnUrl;

                    return viewModel;
                }
            }

            return new ArticleDeleteViewModel(userContext, new ArticleJournalViewModel())
            {
                ReturnUrl = model?.ReturnUrl
            };
        }

        private async Task<ArticleJournalViewModel[]> GetArticleJournalsViewModelsAsync()
        {
            var articleJournals = await this.articlesDataService.GetArticleJournalsAsync().ConfigureAwait(false);

            return articleJournals?.Select(this.mapper.Map<IArticleJournalModel, ArticleJournalViewModel>).ToArray() ?? new ArticleJournalViewModel[] { };
        }
    }
}
