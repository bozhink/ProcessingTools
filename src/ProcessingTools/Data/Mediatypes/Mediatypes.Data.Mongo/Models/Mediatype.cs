namespace ProcessingTools.Mediatypes.Data.Mongo.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Mediatypes;

    public class Mediatype : IStringIdentifiable, IMediatypeEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonIgnoreIfDefault]
        public string Description { get; set; }

        public string FileExtension { get; set; }

        public string Mimesubtype { get; set; }

        public string Mimetype { get; set; }
    }
}
