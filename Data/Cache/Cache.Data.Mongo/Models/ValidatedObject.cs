namespace ProcessingTools.Cache.Data.Mongo.Models
{
    using System.Collections.Generic;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Cache.Data.Common.Contracts.Models;
    using ProcessingTools.Contracts.Models;

    public class ValidatedObject : IStringIdentifiable
    {
        public ValidatedObject()
        {
            this.Values = new List<IValidationCacheEntity>();
        }

        public ValidatedObject(string key, IValidationCacheEntity value)
            : this()
        {
            this.Id = key;
            this.Values.Add(value);
        }

        [BsonId]
        public string Id { get; set; }

        public ICollection<IValidationCacheEntity> Values { get; set; }
    }
}
