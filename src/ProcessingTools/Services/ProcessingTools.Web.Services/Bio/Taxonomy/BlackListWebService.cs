// <copyright file="BlackListWebService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Web.Models.Bio.Taxonomy.BlackList;
    using ProcessingTools.Web.Services.Contracts.Bio.Taxonomy;

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
        public async Task<object> InsertAsync(ItemsRequestModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var items = model.Items?.Select(i => i.Content).ToArray();
            if (items == null || !items.Any())
            {
                return null;
            }

            var result = await this.service.InsertAsync(items).ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc/>
        public async Task<SearchResponseModel> SearchAsync(SearchRequestModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var foundItems = await this.service.SearchAsync(model.SearchString).ConfigureAwait(false);

            if (foundItems != null && foundItems.Any())
            {
                var items = foundItems
                    .Select(i => new ItemResponseModel
                    {
                        Content = i
                    })
                    .ToArray();

                return new SearchResponseModel(items);
            }

            return null;
        }
    }
}
