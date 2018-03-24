namespace ProcessingTools.Cache.Data.Mongo.Models
{
    using System;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts.Cache;

    public class ValidationCacheEntity : IValidationCacheModel
    {
        public ValidationCacheEntity()
        {
        }

        public ValidationCacheEntity(IValidationCacheModel entity)
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
