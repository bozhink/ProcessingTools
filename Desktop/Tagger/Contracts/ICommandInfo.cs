namespace ProcessingTools.Tagger.Contracts
{
    using System;

    public interface ICommandInfo
    {
        Type CommandType { get; set; }

        string Name { get; set; }

        string Description { get; set; }
    }
}
