// <copyright file="BlackListWebService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Api.Contracts;
    using ProcessingTools.Bio.Taxonomy.Api.Models;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

    /// <summary>
    /// Blacklist web service.
    /// </summary>
    public class BlackListWebService : IBlackListWebService
    {
        private readonly IBlackListDataService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlackListWebService"/> class.
        /// </summary>
        /// <param name="service">Data service.</param>
        public BlackListWebService(IBlackListDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <inheritdoc/>
        public async Task<object> InsertAsync(BlackListItemsRequestModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var items = model.Items?.Select(i => i.Content).ToArray();
            if (items is null || !items.Any())
            {
                return null;
            }

            var result = await this.service.InsertAsync(items).ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc/>
        public async Task<BlackListSearchResponseModel> SearchAsync(BlackListSearchRequestModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var foundItems = await this.service.SearchAsync(model.SearchString).ConfigureAwait(false);

            if (foundItems != null && foundItems.Any())
            {
                var items = foundItems
                    .Select(i => new BlackListItemResponseModel
                    {
                        Content = i,
                    })
                    .ToArray();

                return new BlackListSearchResponseModel(items);
            }

            return null;
        }
    }
}
