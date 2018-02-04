namespace ProcessingTools.Cache.Data.Mongo.Models
{
    using System.Collections.Generic;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Models.Contracts;

    public class ValidatedObject : IStringIdentifiable
    {
        public ValidatedObject()
        {
            this.Values = new List<ValidationCacheEntity>();
        }

        [BsonId]
        public string Id { get; set; }

        public ICollection<ValidationCacheEntity> Values { get; set; }
    }
}
