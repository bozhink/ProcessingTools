// <copyright file="CommandInfoProvider.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using ProcessingTools.Commands.Models;
    using ProcessingTools.Commands.Models.Contracts;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Command info provider.
    /// </summary>
    public class CommandInfoProvider : ICommandInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandInfoProvider"/> class.
        /// </summary>
        public CommandInfoProvider()
        {
            this.CommandsInformation = new Dictionary<Type, ICommandInfo>();
        }

        /// <inheritdoc/>
        public IDictionary<Type, ICommandInfo> CommandsInformation { get; private set; }

        /// <inheritdoc/>
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
