namespace ProcessingTools.Tagger.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Contracts;
    using Contracts.Commands;
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
            string defaultInterfaceName = typeof(ITaggerController).FullName;
            var types = System.Reflection.Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsGenericType && !t.IsAbstract)
                .Where(t => t.GetInterfaces().Any(i => i.FullName == defaultInterfaceName))
                .ToArray();

            types.Select(t =>
            {
                string name = Regex.Match(t.FullName, @"(?<=\A.*?)([^\.]+)(?=Command\Z)").Value;
                return new CommandInfo
                {
                    CommandType = t,
                    Name = name,
                    Description = t.GetDescriptionMessageForCommand()
                };
            })
            .ToList()
            .ForEach(o =>
            {
                this.CommandsInformation.Add(o.CommandType, o);
            });
        }
    }
}
