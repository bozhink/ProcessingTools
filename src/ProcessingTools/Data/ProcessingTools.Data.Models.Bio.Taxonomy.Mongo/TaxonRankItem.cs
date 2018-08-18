// <copyright file="TaxonRankItem.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Bio.Taxonomy.Mongo
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Attributes;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// MongoDB implementation of <see cref="ITaxonRankItem"/>.
    /// </summary>
    [CollectionName("taxa")]
    public class TaxonRankItem : ITaxonRankItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonRankItem"/> class.
        /// </summary>
        public TaxonRankItem()
        {
            this.Ranks = new HashSet<TaxonRankType>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonRankItem"/> class.
        /// </summary>
        /// <param name="item">Taxon rank item.</param>
        public TaxonRankItem(ITaxonRankItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            this.Name = item.Name;
            this.IsWhiteListed = item.IsWhiteListed;
            this.Ranks = item.Ranks;
        }

        /// <summary>
        /// Gets or sets the _id.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the taxon.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the taxon name has to be included in the white list.
        /// </summary>
        public bool IsWhiteListed { get; set; }

        /// <summary>
        /// Gets or sets the collection of taxon ranks for the taxon.
        /// </summary>
        public ICollection<TaxonRankType> Ranks { get; set; }
    }
}
