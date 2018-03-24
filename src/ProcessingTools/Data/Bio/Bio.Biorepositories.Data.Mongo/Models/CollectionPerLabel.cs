namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Bio.Biorepositories;

    public class CollectionPerLabel : IStringIdentifiable, ICollectionPerLabel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        [BsonIgnoreIfDefault]
        public string CityTown { get; set; }

        [BsonIgnoreIfDefault]
        public string CollectionName { get; set; }

        [BsonIgnoreIfDefault]
        public string Country { get; set; }

        [BsonIgnoreIfDefault]
        public string PostalZipCode { get; set; }

        [BsonIgnoreIfDefault]
        public string PrimaryContact { get; set; }

        [BsonIgnoreIfDefault]
        public string StateProvince { get; set; }
    }
}
