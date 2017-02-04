namespace ProcessingTools.Tagger.Commands.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Contracts.Commands;
    using Contracts.Models;
    using Contracts.Providers;
    using Extensions;
    using Models;

    public class CommandInfoProvider : ICommandInfoProvider
    {
        public CommandInfoProvider()
        {
            this.CommandsInformation = new Dictionary<Type, ICommandInfo>();
        }

        public IDictionary<Type, ICommandInfo> CommandsInformation { get; private set; }

        public void ProcessInformation()
        {
            // Print commands’ information
            var commandType = typeof(ITaggerCommand);
            var types = commandType.Assembly
                .GetTypes()
                .Where(t => t.IsClass && !t.IsGenericType && !t.IsAbstract && t.GetInterfaces().Contains(commandType))
                .ToArray();

            types.Select(type =>
            {
                string name = Regex.Match(type.FullName, @"(?<=\A.*?)([^\.]+)(?=Command\Z)").Value;
                return new CommandInfo
                {
                    CommandType = type,
                    Name = name,
                    Description = type.GetDescriptionMessageForCommand()
                };
            })
            .ToList()
            .ForEach(commandInfo =>
            {
                this.CommandsInformation.Add(commandInfo.CommandType, commandInfo);
            });
        }
    }
}
