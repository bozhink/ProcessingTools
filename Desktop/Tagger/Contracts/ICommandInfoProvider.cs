namespace ProcessingTools.Tagger.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface ICommandInfoProvider
    {
        IDictionary<Type, ICommandInfo> CommandsInformation { get; }

        void ProcessInformation();
    }
}
