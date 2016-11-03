﻿namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Collections.Generic;

    public interface IKeyListableRepository<TKey>
    {
        IEnumerable<TKey> Keys { get; }
    }
}
