// <copyright file="TaxonRankTypeItem.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Bio.Taxonomy.Mongo
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Attributes;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Taxon rank type item.
    /// </summary>
    [CollectionName("taxonRankType")]
    public class TaxonRankTypeItem : INameableStringIdentifiable
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
