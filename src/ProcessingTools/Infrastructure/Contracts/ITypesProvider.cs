namespace ProcessingTools.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface ITypesProvider
    {
        IEnumerable<Type> GetTypes();
    }
}
