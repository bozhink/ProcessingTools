namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Data.Common.Mongo.Attributes;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

    [CollectionName("blackList")]
    public class MongoBlackListEntity : IBlackListEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Content { get; set; }
    }
}
