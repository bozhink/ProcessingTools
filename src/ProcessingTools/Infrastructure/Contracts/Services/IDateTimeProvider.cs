namespace ProcessingTools.Contracts.Services
{
    using System;

    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
