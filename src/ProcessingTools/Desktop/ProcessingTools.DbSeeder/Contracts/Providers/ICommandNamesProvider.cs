namespace ProcessingTools.DbSeeder.Contracts.Providers
{
    using System.Collections.Generic;

    public interface ICommandNamesProvider
    {
        IEnumerable<string> CommandNames { get; }
    }
}
