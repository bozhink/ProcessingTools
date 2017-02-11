namespace ProcessingTools.Tagger.Commands.Contracts.Models
{
    using System;

    public interface ICommandInfo
    {
        Type CommandType { get; set; }

        string Name { get; set; }

        string Description { get; set; }
    }
}
