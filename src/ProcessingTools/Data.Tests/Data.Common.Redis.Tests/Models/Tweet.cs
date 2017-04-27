namespace ProcessingTools.Data.Common.Redis.Tests.Models
{
    using System;

    public class Tweet : ITweet
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
