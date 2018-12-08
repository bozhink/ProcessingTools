namespace ProcessingTools.Data.Memory.Unit.Tests.Models
{
    using System;

    public interface ITweet
    {
        int Id { get; }

        string Content { get; }

        DateTime PostedOn { get; }
    }
}
