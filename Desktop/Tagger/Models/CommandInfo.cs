namespace ProcessingTools.Tagger.Models
{
    using System;
    using Contracts;

    public class CommandInfo : ICommandInfo
    {
        public Type CommandType { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }
    }
}
