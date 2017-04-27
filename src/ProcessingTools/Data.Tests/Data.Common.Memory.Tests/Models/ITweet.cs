namespace ProcessingTools.Data.Common.Memory.Tests.Models
{
    using System;

    public interface ITweet
    {
        string Content { get; }

        int Id { get; }

        DateTime PostedOn { get; }
    }
}
