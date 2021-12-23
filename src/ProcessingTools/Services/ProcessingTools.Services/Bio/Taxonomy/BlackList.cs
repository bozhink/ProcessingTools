// <copyright file="BlackList.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.DataAccess.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

    /// <summary>
    /// Taxonomic blacklist.
    /// </summary>
    public class BlackList : IBlackList
    {
        private readonly IBlackListDataAccessObject dataAccessObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlackList"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Data access object.</param>
        public BlackList(IBlackListDataAccessObject dataAccessObject)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
        }

        /// <inheritdoc/>
        public Task<IList<string>> GetItemsAsync() => this.dataAccessObject.GetAllAsync();
    }
}
