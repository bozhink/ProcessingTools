namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Models
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Data.Common.Mongo.Attributes;
    using ProcessingTools.Enumerations;

    [CollectionName("taxa")]
    public class MongoTaxonRankEntity : ITaxonRankEntity
    {
        public MongoTaxonRankEntity()
        {
            this.Ranks = new HashSet<TaxonRankType>();
        }

        public MongoTaxonRankEntity(ITaxonRankEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.Name = entity.Name;
            this.IsWhiteListed = entity.IsWhiteListed;
            this.Ranks = entity.Ranks;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsWhiteListed { get; set; }

        public ICollection<TaxonRankType> Ranks { get; set; }
    }
}
