namespace ProcessingTools.Data.Common.Tests.Models
{
    using System;

    public interface ITweet
    {
        int Id { get; }

        string Content { get; set; }

        DateTime DatePosted { get; set; }

        int Faves { get; set; }
    }
}
