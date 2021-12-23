﻿// <copyright file="FileProcessor.Helpers.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tagger.Core
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Extensions;

    public partial class FileProcessor
    {
        private static async Task InvokeProcessor(string message, Func<Task> action, ILogger logger)
        {
            var timer = new Stopwatch();
            timer.Start();

            try
            {
                logger.LogDebug(message: message);
                await action.Invoke().ConfigureAwait(false);
            }
            catch (AggregateException e)
            {
                foreach (var exception in e.InnerExceptions)
                {
                    logger.LogError(exception, string.Empty);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, string.Empty);
            }

            logger.LogDebug(Messages.ElapsedTimeMessageFormat, timer.Elapsed);
        }

        private async Task InvokeProcessor<TCommand>(XmlNode context)
            where TCommand : ITaggerCommand
        {
            await this.InvokeProcessor(typeof(TCommand), context).ConfigureAwait(false);
        }

        private async Task InvokeProcessor(Type commandType, XmlNode context)
        {
            var command = this.commandFactory(commandType);
            var document = this.documentWrapper.Create(context, this.settings.ArticleSchemaType);

            var isNotAwaitableCommand = commandType.GetInterfaces().Any(t => t == typeof(INotAwaitableCommand));
            if (isNotAwaitableCommand)
            {
                // Validation commands should not overwrite the content of this.document.XmlDocument,
                // and here this content is copied in a new DOM object.
                var task = this.InvokeCommand(command, document);
                this.tasks.Enqueue(task);
            }
            else
            {
                await this.InvokeCommand(command, document).ConfigureAwait(false);
                context.InnerXml = document.XmlDocument.DocumentElement.InnerXml;
            }
        }

        private Task InvokeCommand(ITaggerCommand command, IDocument document)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command), $"Command of type {command.GetType().FullName} is invalid.");
            }

            string message = $"\n\t{command.GetType().GetDescriptionMessageForCommand()}\n";
            return InvokeProcessor(
                message,
                () => command.RunAsync(document, this.settings),
                this.logger);
        }
    }
}
