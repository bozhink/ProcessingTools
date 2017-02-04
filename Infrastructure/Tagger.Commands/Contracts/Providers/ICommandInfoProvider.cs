namespace ProcessingTools.Tagger.Commands.Contracts.Providers
{
    using System;
    using System.Collections.Generic;
    using Models;

    public interface ICommandInfoProvider
    {
        IDictionary<Type, ICommandInfo> CommandsInformation { get; }

        void ProcessInformation();
    }
}
