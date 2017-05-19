namespace ProcessingTools.Contracts.Services
{
    using System;

    public interface IDateTimeProvider : IService
    {
        DateTime Now { get; }
    }
}
