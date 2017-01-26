namespace ProcessingTools.Cache.Data.Mongo.Models
{
    using System;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Cache.Data.Common.Contracts.Models;
    using ProcessingTools.Enumerations;

    public class ValidationCacheEntity : IValidationCacheEntity
    {
        [BsonIgnoreIfDefault]
        public string Content { get; set; }

        public DateTime LastUpdate { get; set; }

        public ValidationStatus Status { get; set; }
    }
}
