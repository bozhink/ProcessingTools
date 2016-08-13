namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Models
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Contracts;

    public class TaxonEntity : INameableStringIdentifiable
    {
        public TaxonEntity()
        {
            this.Ranks = new HashSet<string>();
        }

        public TaxonEntity(TaxonRankEntity entity)
            : this()
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.Name = entity.Name;
            this.IsWhiteListed = entity.IsWhiteListed;
            foreach (var rank in entity.Ranks)
            {
                this.Ranks.Add(rank.MapTaxonRankTypeToTaxonRankString());
            }
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsWhiteListed { get; set; }

        public ICollection<string> Ranks { get; set; }
    }
}
