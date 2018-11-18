namespace ProcessingTools.Data.Models.Mongo.Bio.Biorepositories
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Bio.Biorepositories;

    public class StaffLabel : IStringIdentifiable, IStaffLabel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        [BsonIgnoreIfDefault]
        public string CityTown { get; set; }

        [BsonIgnoreIfDefault]
        public string Country { get; set; }

        [BsonIgnoreIfDefault]
        public string PostalZipCode { get; set; }

        [BsonIgnoreIfDefault]
        public string PrimaryInstitution { get; set; }

        [BsonIgnoreIfDefault]
        public string StaffMemberFullName { get; set; }

        [BsonIgnoreIfDefault]
        public string StateProvince { get; set; }
    }
}
