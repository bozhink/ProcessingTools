namespace ProcessingTools.Tagger.Commands.Models
{
    using System;
    using ProcessingTools.Contracts.Commands;

    public class CommandInfo : ICommandInfo
    {
        public Type CommandType { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }
    }
}
