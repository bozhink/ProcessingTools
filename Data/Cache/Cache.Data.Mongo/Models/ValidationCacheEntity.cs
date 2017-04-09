namespace ProcessingTools.Cache.Data.Mongo.Models
{
    using System;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Contracts.Data.Cache.Models;
    using ProcessingTools.Enumerations;

    public class ValidationCacheEntity : IValidationCacheEntity
    {
        public ValidationCacheEntity()
        {
        }

        public ValidationCacheEntity(IValidationCacheEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.Content = entity.Content;
            this.LastUpdate = entity.LastUpdate;
            this.Status = entity.Status;
        }

        [BsonIgnoreIfDefault]
        public string Content { get; set; }

        public DateTime LastUpdate { get; set; }

        public ValidationStatus Status { get; set; }
    }
}
