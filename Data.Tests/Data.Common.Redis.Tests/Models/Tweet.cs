namespace ProcessingTools.Data.Common.Redis.Tests.Models
{
    using System;
    using ProcessingTools.Contracts;

    public class Tweet : IEntity, ITweet
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
