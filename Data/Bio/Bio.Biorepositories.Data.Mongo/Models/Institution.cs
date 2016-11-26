namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    using ProcessingTools.Contracts;

    public class Institution : IStringIdentifiable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        [BsonIgnoreIfDefault]
        public string AdditionalInstitutionNames { get; set; }

        [BsonIgnoreIfDefault]
        public string CitesPermitNumber { get; set; }

        [BsonIgnoreIfDefault]
        public string CoolUri { get; set; }

        [BsonIgnoreIfDefault]
        public string DateHerbariumWasFounded { get; set; }

        [BsonIgnoreIfDefault]
        public string DescriptionOfInstitution { get; set; }

        [BsonIgnoreIfDefault]
        public string GeographicCoverageOfHerbarium { get; set; }

        [BsonIgnoreIfDefault]
        public string IncorporatedHerbaria { get; set; }

        public IndexHerbariorumRecordType IndexHerbariorumRecord { get; set; }

        [BsonIgnoreIfDefault]
        public string InstitutionCode { get; set; }

        [BsonIgnoreIfDefault]
        public string InstitutionName { get; set; }

        [BsonIgnoreIfDefault]
        public string InstitutionType { get; set; }

        [BsonIgnoreIfDefault]
        public string InstitutionalDiscipline { get; set; }

        [BsonIgnoreIfDefault]
        public string InstitutionalGovernance { get; set; }

        [BsonIgnoreIfDefault]
        public string InstitutionalLsid { get; set; }

        [BsonIgnoreIfDefault]
        public string NumberOfSpecimensInHerbarium { get; set; }

        [BsonIgnoreIfDefault]
        public string PrimaryContact { get; set; }

        [BsonIgnoreIfDefault]
        public string SerialsPublishedByInstitution { get; set; }

        [BsonIgnoreIfDefault]
        public string StatusOfInstitution { get; set; }

        [BsonIgnoreIfDefault]
        public string TaxonomicCoverageOfHerbarium { get; set; }

        [BsonIgnoreIfDefault]
        public string UrlForInstitutionalSpecimenCatalog { get; set; }

        [BsonIgnoreIfDefault]
        public string UrlForInstitutionalWebservices { get; set; }

        [BsonIgnoreIfDefault]
        public string UrlForMainInstitutionalWebsite { get; set; }

        [BsonIgnoreIfDefault]
        public string Url { get; set; }
    }
}