namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Bio.Biorepositories.Data.Common.Contracts.Models;
    using ProcessingTools.Contracts.Models;

    public class Staff : IStringIdentifiable, IStaff
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        [BsonIgnoreIfDefault]
        public string AdditionalAffiliations { get; set; }

        [BsonIgnoreIfDefault]
        public string AreaOfResponsibility { get; set; }

        [BsonIgnoreIfDefault]
        public string BirthYear { get; set; }

        [BsonIgnoreIfDefault]
        public string CityTown { get; set; }

        [BsonIgnoreIfDefault]
        public string Country { get; set; }

        [BsonIgnoreIfDefault]
        public string Email { get; set; }

        [BsonIgnoreIfDefault]
        public string FaxNumber { get; set; }

        [BsonIgnoreIfDefault]
        public string FirstName { get; set; }

        [BsonIgnoreIfDefault]
        public string JobTitle { get; set; }

        [BsonIgnoreIfDefault]
        public string LastName { get; set; }

        [BsonIgnoreIfDefault]
        public string PhoneNumber { get; set; }

        [BsonIgnoreIfDefault]
        public string PostalZipCode { get; set; }

        [BsonIgnoreIfDefault]
        public string PrimaryCollection { get; set; }

        [BsonIgnoreIfDefault]
        public string PrimaryInstitution { get; set; }

        [BsonIgnoreIfDefault]
        public string ResearchDiscipline { get; set; }

        [BsonIgnoreIfDefault]
        public string ResearchSpecialty { get; set; }

        [BsonIgnoreIfDefault]
        public string StaffMemberFullName { get; set; }

        [BsonIgnoreIfDefault]
        public string StateProvince { get; set; }

        [BsonIgnoreIfDefault]
        public string Url { get; set; }
    }
}
