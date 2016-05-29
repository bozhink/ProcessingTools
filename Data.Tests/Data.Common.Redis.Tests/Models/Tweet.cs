namespace ProcessingTools.Data.Common.Redis.Tests.Models
{
    using System;
    using ProcessingTools.Data.Common.Models.Contracts;

    public class Tweet : IEntity
    {
        public Tweet()
        {
            this.PostedOn = DateTime.UtcNow;
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime PostedOn { get; set; }
    }
}
