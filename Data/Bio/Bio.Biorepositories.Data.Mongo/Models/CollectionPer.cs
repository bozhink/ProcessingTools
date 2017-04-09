namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Contracts.Data.Bio.Biorepositories.Models;
    using ProcessingTools.Contracts.Models;

    public class CollectionPer : IStringIdentifiable, ICollectionPer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        [BsonIgnoreIfDefault]
        public string AccessEligibilityAndRules { get; set; }

        [BsonIgnoreIfDefault]
        public string CollectionCode { get; set; }

        [BsonIgnoreIfDefault]
        public string CollectionContentType { get; set; }

        [BsonIgnoreIfDefault]
        public string CollectionDescription { get; set; }

        [BsonIgnoreIfDefault]
        public string CollectionName { get; set; }

        [BsonIgnoreIfDefault]
        public string CoolUri { get; set; }

        [BsonIgnoreIfDefault]
        public string InstitutionName { get; set; }

        [BsonIgnoreIfDefault]
        public string Lsid { get; set; }

        [BsonIgnoreIfDefault]
        public string PreservationType { get; set; }

        [BsonIgnoreIfDefault]
        public string PrimaryContact { get; set; }

        [BsonIgnoreIfDefault]
        public string StatusOfCollection { get; set; }

        [BsonIgnoreIfDefault]
        public string UrlForCollection { get; set; }

        [BsonIgnoreIfDefault]
        public string UrlForCollectionSpecimenCatalog { get; set; }

        [BsonIgnoreIfDefault]
        public string UrlForCollectionWebservices { get; set; }

        [BsonIgnoreIfDefault]
        public string Url { get; set; }
    }
}
