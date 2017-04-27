namespace ProcessingTools.Common.Providers
{
    using System;
    using ProcessingTools.Contracts;

    public class GuidProvider : IGuidProvider
    {
        public Guid NewGuid() => Guid.NewGuid();
    }
}
