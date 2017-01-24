namespace ProcessingTools.Documents.Data.Mongo.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Documents.Data.Common.Contracts.Models;

    public class JournalMeta : IStringIdentifiable, IJournalMeta
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        [BsonIgnoreIfDefault]
        public string AbbreviatedJournalTitle { get; set; }

        [BsonIgnoreIfDefault]
        public string ArchiveNamePattern { get; set; }

        [BsonIgnoreIfDefault]
        public string FileNamePattern { get; set; }

        [BsonIgnoreIfDefault]
        public string IssnEPub { get; set; }

        [BsonIgnoreIfDefault]
        public string IssnPPub { get; set; }

        [BsonIgnoreIfDefault]
        public string JournalId { get; set; }

        [BsonIgnoreIfDefault]
        public string JournalTitle { get; set; }

        [BsonIgnoreIfDefault]
        public string Permalink { get; set; }

        [BsonIgnoreIfDefault]
        public string PublisherName { get; set; }
    }
}
