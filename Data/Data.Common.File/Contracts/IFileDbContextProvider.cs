using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingTools.Data.Common.File.Contracts
{
    using ProcessingTools.Data.Common.Contracts;

    public interface IFileDbContextProvider<TContext, T> : IDatabaseProvider<TContext>
        where TContext : IFileDbContext<T>
    {
    }
}
