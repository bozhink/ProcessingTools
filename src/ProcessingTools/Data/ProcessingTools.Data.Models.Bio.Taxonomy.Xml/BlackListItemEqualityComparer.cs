// <copyright file="BlackListItemEqualityComparer.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Bio.Taxonomy.Xml
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Black list item equality comparer.
    /// </summary>
    public class BlackListItemEqualityComparer : EqualityComparer<IBlackListItem>
    {
        /// <inheritdoc/>
        public override bool Equals(IBlackListItem x, IBlackListItem y) => (x?.Content ?? string.Empty) == (y?.Content ?? string.Empty);

        /// <inheritdoc/>
        public override int GetHashCode(IBlackListItem obj) => obj?.Content.GetHashCode() ?? -1;
    }
}
