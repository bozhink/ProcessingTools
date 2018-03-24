// <copyright file="JournalsService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Documents
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Web.Models.Documents.Journals;
    using ProcessingTools.Web.Models.Shared;
    using ProcessingTools.Web.Services.Contracts.Documents;

    /// <summary>
    /// Journals service.
    /// </summary>
    public class JournalsService : IJournalsService
    {
        private readonly IJournalsDataService journalsDataService;
        private readonly Func<Task<UserContext>> userContextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="JournalsService"/> class.
        /// </summary>
        /// <param name="journalsDataService">Instance of <see cref="IJournalsDataService"/>.</param>
        /// <param name="userContext">User context.</param>
        public JournalsService(IJournalsDataService journalsDataService, IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.journalsDataService = journalsDataService ?? throw new ArgumentNullException(nameof(journalsDataService));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));
        }

        /// <inheritdoc/>
        public async Task<bool> CreateJournalAsync(JournalCreateRequestModel model)
        {
            if (model == null)
            {
                return false;
            }

            var result = await this.journalsDataService.InsertAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteJournalAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            var result = await this.journalsDataService.DeleteAsync(id).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateJournalAsync(JournalUpdateRequestModel model)
        {
            if (model == null)
            {
                return false;
            }

            var result = await this.journalsDataService.UpdateAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<JournalCreateViewModel> GetJournalCreateViewModelAsync()
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            var publishers = await this.GetJournalPublishersViewModelsAsync().ConfigureAwait(false);

            return new JournalCreateViewModel(userContext, publishers);
        }

        /// <inheritdoc/>
        public async Task<JournalDeleteViewModel> GetJournalDeleteViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var journal = await this.journalsDataService.GetDetailsById(id).ConfigureAwait(false);
                if (journal != null)
                {
                    var publisher = this.MapJournalPublisherToViewModel(journal);

                    return new JournalDeleteViewModel(userContext, publisher)
                    {
                        Id = journal.Id,
                        Name = journal.Name,
                        AbbreviatedName = journal.AbbreviatedName,
                        JournalId = journal.JournalId,
                        PrintIssn = journal.PrintIssn,
                        ElectronicIssn = journal.ElectronicIssn,
                        CreatedBy = journal.CreatedBy,
                        CreatedOn = journal.CreatedOn,
                        ModifiedBy = journal.ModifiedBy,
                        ModifiedOn = journal.ModifiedOn
                    };
                }
            }

            return new JournalDeleteViewModel(userContext, new JournalPublisherViewModel());
        }

        /// <inheritdoc/>
        public async Task<JournalDetailsViewModel> GetJournalDetailsViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var journal = await this.journalsDataService.GetDetailsById(id).ConfigureAwait(false);
                if (journal != null)
                {
                    var publisher = this.MapJournalPublisherToViewModel(journal);

                    return new JournalDetailsViewModel(userContext, publisher)
                    {
                        Id = journal.Id,
                        Name = journal.Name,
                        AbbreviatedName = journal.AbbreviatedName,
                        JournalId = journal.JournalId,
                        PrintIssn = journal.PrintIssn,
                        ElectronicIssn = journal.ElectronicIssn,
                        CreatedBy = journal.CreatedBy,
                        CreatedOn = journal.CreatedOn,
                        ModifiedBy = journal.ModifiedBy,
                        ModifiedOn = journal.ModifiedOn
                    };
                }
            }

            return new JournalDetailsViewModel(userContext, new JournalPublisherViewModel());
        }

        /// <inheritdoc/>
        public async Task<JournalEditViewModel> GetJournalEditViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var journal = await this.journalsDataService.GetDetailsById(id).ConfigureAwait(false);
                if (journal != null)
                {
                    var publishers = await this.GetJournalPublishersViewModelsAsync(journal).ConfigureAwait(false);

                    return new JournalEditViewModel(userContext, publishers)
                    {
                        Id = journal.Id,
                        Name = journal.Name,
                        AbbreviatedName = journal.AbbreviatedName,
                        JournalId = journal.JournalId,
                        PrintIssn = journal.PrintIssn,
                        ElectronicIssn = journal.ElectronicIssn,
                        PublisherId = journal.PublisherId,
                        CreatedBy = journal.CreatedBy,
                        CreatedOn = journal.CreatedOn,
                        ModifiedBy = journal.ModifiedBy,
                        ModifiedOn = journal.ModifiedOn
                    };
                }
            }

            return new JournalEditViewModel(userContext, new JournalPublisherViewModel[] { });
        }

        /// <inheritdoc/>
        public async Task<JournalsIndexViewModel> GetJournalsIndexViewModelAsync(int skip, int take)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            var data = await this.journalsDataService.SelectDetailsAsync(skip, take).ConfigureAwait(false);
            var count = await this.journalsDataService.SelectCountAsync().ConfigureAwait(false);

            var journals = data?.Select(j => new JournalIndexViewModel
            {
                Id = j.Id,
                Name = j.Name,
                AbbreviatedName = j.AbbreviatedName,
                JournalId = j.JournalId,
                PrintIssn = j.PrintIssn,
                ElectronicIssn = j.ElectronicIssn,
                CreatedBy = j.CreatedBy,
                CreatedOn = j.CreatedOn,
                ModifiedBy = j.ModifiedBy,
                ModifiedOn = j.ModifiedOn,
                Publisher = new JournalPublisherViewModel
                {
                    Id = j.Publisher.Id,
                    Name = j.Publisher.Name,
                    AbbreviatedName = j.Publisher.AbbreviatedName,
                    Selected = true
                }
            });

            return new JournalsIndexViewModel(userContext, count, take, skip / take, journals ?? new JournalIndexViewModel[] { });
        }

        /// <inheritdoc/>
        public async Task<JournalCreateViewModel> MapToViewModelAsync(JournalCreateRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            var publishers = await this.GetJournalPublishersViewModelsAsync().ConfigureAwait(false);

            if (model != null)
            {
                return new JournalCreateViewModel(userContext, publishers)
                {
                    Name = model.Name,
                    AbbreviatedName = model.AbbreviatedName,
                    JournalId = model.JournalId,
                    PrintIssn = model.PrintIssn,
                    ElectronicIssn = model.ElectronicIssn,
                    ReturnUrl = model.ReturnUrl
                };
            }

            return new JournalCreateViewModel(userContext, publishers)
            {
                ReturnUrl = model?.ReturnUrl
            };
        }

        /// <inheritdoc/>
        public async Task<JournalEditViewModel> MapToViewModelAsync(JournalUpdateRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var journal = await this.journalsDataService.GetDetailsById(model.Id).ConfigureAwait(false);
                if (journal != null)
                {
                    var publishers = await this.GetJournalPublishersViewModelsAsync(journal).ConfigureAwait(false);

                    return new JournalEditViewModel(userContext, publishers)
                    {
                        Id = model.Id,
                        Name = model.Name,
                        AbbreviatedName = model.AbbreviatedName,
                        JournalId = model.JournalId,
                        PrintIssn = model.PrintIssn,
                        ElectronicIssn = model.ElectronicIssn,
                        CreatedBy = journal.CreatedBy,
                        CreatedOn = journal.CreatedOn,
                        ModifiedBy = journal.ModifiedBy,
                        ModifiedOn = journal.ModifiedOn,
                        ReturnUrl = model.ReturnUrl
                    };
                }
            }

            return new JournalEditViewModel(userContext, new JournalPublisherViewModel[] { })
            {
                ReturnUrl = model?.ReturnUrl
            };
        }

        /// <inheritdoc/>
        public async Task<JournalDeleteViewModel> MapToViewModelAsync(JournalDeleteRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var journal = await this.journalsDataService.GetDetailsById(model.Id).ConfigureAwait(false);
                if (journal != null)
                {
                    var publisher = this.MapJournalPublisherToViewModel(journal);

                    return new JournalDeleteViewModel(userContext, publisher)
                    {
                        Id = journal.Id,
                        Name = journal.Name,
                        AbbreviatedName = journal.AbbreviatedName,
                        JournalId = journal.JournalId,
                        PrintIssn = journal.PrintIssn,
                        ElectronicIssn = journal.ElectronicIssn,
                        CreatedBy = journal.CreatedBy,
                        CreatedOn = journal.CreatedOn,
                        ModifiedBy = journal.ModifiedBy,
                        ModifiedOn = journal.ModifiedOn,
                        ReturnUrl = model.ReturnUrl
                    };
                }
            }

            return new JournalDeleteViewModel(userContext, new JournalPublisherViewModel())
            {
                ReturnUrl = model?.ReturnUrl
            };
        }

        private JournalPublisherViewModel MapJournalPublisherToViewModel(ProcessingTools.Services.Models.Contracts.Documents.Journals.IJournalDetailsModel journal)
        {
            return new JournalPublisherViewModel
            {
                Id = journal.Publisher?.Id,
                Name = journal.Publisher?.Name,
                AbbreviatedName = journal.Publisher?.AbbreviatedName,
                Selected = true
            };
        }

        private async Task<JournalPublisherViewModel[]> GetJournalPublishersViewModelsAsync()
        {
            var journalPublishers = await this.journalsDataService.GetJournalPublishersAsync().ConfigureAwait(false);

            return journalPublishers?.Select(p => new JournalPublisherViewModel
            {
                Id = p.Id,
                Name = p.Name,
                AbbreviatedName = p.AbbreviatedName,
                Selected = false
            }).ToArray() ?? new JournalPublisherViewModel[] { };
        }

        private async Task<JournalPublisherViewModel[]> GetJournalPublishersViewModelsAsync(ProcessingTools.Services.Models.Contracts.Documents.Journals.IJournalDetailsModel journal)
        {
            var journalPublishers = await this.journalsDataService.GetJournalPublishersAsync().ConfigureAwait(false);

            return journalPublishers?.Select(p => new JournalPublisherViewModel
            {
                Id = p.Id,
                Name = p.Name,
                AbbreviatedName = p.AbbreviatedName,
                Selected = journal.Publisher != null && p.Name == journal.Publisher.Name && p.AbbreviatedName == journal.Publisher.AbbreviatedName
            }).ToArray() ?? new JournalPublisherViewModel[] { };
        }
    }
}
