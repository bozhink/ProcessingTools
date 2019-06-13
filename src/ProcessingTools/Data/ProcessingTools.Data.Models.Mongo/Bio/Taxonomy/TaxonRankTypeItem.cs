﻿// <copyright file="TaxonRankTypeItem.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Bio.Taxonomy
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Taxon rank type item.
    /// </summary>
    [CollectionName("taxonRankType")]
    public class TaxonRankTypeItem : INamedStringIdentified
    {
        private TaxonRankType rankType;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonRankTypeItem"/> class.
        /// </summary>
        public TaxonRankTypeItem()
        {
            this.rankType = TaxonRankType.Other;
        }

        /// <summary>
        /// Gets or sets the _id.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the taxon rank.
        /// </summary>
        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the taxon rank name.
        /// </summary>
        [BsonRepresentation(BsonType.Int32)]
        public TaxonRankType RankType
        {
            get
            {
                return this.rankType;
            }

            set
            {
                this.rankType = value;
                this.Name = this.rankType.MapTaxonRankTypeToTaxonRankString();
            }
        }
    }
}
