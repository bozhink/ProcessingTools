namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Attributes;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Models.Contracts;

    [CollectionName("taxonRankType")]
    public class MongoTaxonRankTypeEntity : INameableStringIdentifiable
    {
        private TaxonRankType rankType;

        public MongoTaxonRankTypeEntity()
        {
            this.rankType = TaxonRankType.Other;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

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
