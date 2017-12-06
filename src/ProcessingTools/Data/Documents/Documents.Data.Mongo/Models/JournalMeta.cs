namespace ProcessingTools.Documents.Data.Mongo.Models
{
    using System.Text.RegularExpressions;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Data.Common.Mongo.Attributes;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Documents;

    [CollectionName("journalsMeta")]
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

        [BsonIgnore]
        public string Permalink => Regex.Replace(this.AbbreviatedJournalTitle, @"\W+", "_").ToLowerInvariant();

        [BsonIgnoreIfDefault]
        public string PublisherName { get; set; }
    }
}
