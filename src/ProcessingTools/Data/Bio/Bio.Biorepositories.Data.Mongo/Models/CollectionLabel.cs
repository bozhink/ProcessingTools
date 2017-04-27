namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Contracts.Data.Bio.Biorepositories.Models;
    using ProcessingTools.Contracts.Models;

    public class CollectionLabel : IStringIdentifiable, ICollectionLabel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        [BsonIgnoreIfDefault]
        public string CollectionName { get; set; }

        [BsonIgnoreIfDefault]
        public string PrimaryContact { get; set; }

        [BsonIgnoreIfDefault]
        public string CityTown { get; set; }

        [BsonIgnoreIfDefault]
        public string StateProvince { get; set; }

        [BsonIgnoreIfDefault]
        public string PostalZipCode { get; set; }

        [BsonIgnoreIfDefault]
        public string Country { get; set; }
    }
}
