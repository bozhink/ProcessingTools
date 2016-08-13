namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Models
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Bio.Taxonomy.Types;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Bson;

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
