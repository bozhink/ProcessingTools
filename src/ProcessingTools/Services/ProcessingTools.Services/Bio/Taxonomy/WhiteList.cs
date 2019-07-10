// <copyright file="WhiteList.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Bio.Taxonomy;

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.DataAccess.Bio.Taxonomy;

    /// <summary>
    /// Taxonomic whitelist.
    /// </summary>
    public class WhiteList : IWhiteList
    {
        private readonly ITaxonRanksDataAccessObject dataAccessObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="WhiteList"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Data access object.</param>
        public WhiteList(ITaxonRanksDataAccessObject dataAccessObject)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
        }

        /// <inheritdoc/>
        public Task<IList<string>> GetItemsAsync() => this.dataAccessObject.GetWhiteListedAsync();
    }
}
