namespace ProcessingTools.Bio.Biorepositories.Data.Models.Bson
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Collection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        [BsonIgnoreIfDefault]
        public string AccessEligibilityAndRules { get; set; }

        [BsonIgnoreIfDefault]
        public string AccessionStatus { get; set; }

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
        public string InstitutionCode { get; set; }

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
        public string Url { get; set; }

        [BsonIgnoreIfDefault]
        public string UrlForCollection { get; set; }

        [BsonIgnoreIfDefault]
        public string UrlForCollectionSpecimenCatalog { get; set; }

        [BsonIgnoreIfDefault]
        public string UrlForCollectionWebservices { get; set; }
    }
}