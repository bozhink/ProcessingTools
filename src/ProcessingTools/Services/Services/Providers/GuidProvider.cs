namespace ProcessingTools.Services.Providers
{
    using System;
    using ProcessingTools.Contracts;

    public class GuidProvider : IGuidProvider
    {
        public Guid NewGuid() => Guid.NewGuid();
    }
}
