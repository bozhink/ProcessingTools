namespace ProcessingTools.Data.Common.Tests.Fakes.Models.Contracts
{
    using System;

    public interface ITweet
    {
        string Content { get; set; }

        DateTime DatePosted { get; set; }

        int Faves { get; set; }
    }
}
