namespace ProcessingTools.Tagger.Core
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts.Commands;
    using Extensions;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;

    public partial class SingleFileProcessor
    {
        private static async Task InvokeProcessor(string message, Func<Task> action, ILogger logger)
        {
            var timer = new Stopwatch();
            timer.Start();
            logger?.Log(message);

            try
            {
                await action.Invoke();
            }
            catch (AggregateException e)
            {
                foreach (var exception in e.InnerExceptions)
                {
                    logger?.Log(exception, string.Empty);
                    logger?.Log();
                }
            }
            catch (Exception e)
            {
                logger?.Log(e, string.Empty);
            }

            logger?.Log(LogType.Info, Messages.ElapsedTimeMessageFormat, timer.Elapsed);
        }

        private async Task InvokeProcessor<TCommand>(XmlNode context)
            where TCommand : ITaggerCommand
        {
            await this.InvokeProcessor(typeof(TCommand), context);
        }

        private async Task InvokeProcessor(Type commandType, XmlNode context)
        {
            var command = this.commandFactory(commandType);
            var document = this.WrapContextInDocument(context);

            var isValidationCommand = commandType.GetInterfaces().Count(t => t == typeof(INotAwaitableCommand)) > 0;
            if (isValidationCommand)
            {
                // Validation commands should not overwrite the content of this.document.XmlDocument,
                // and here this content is copied in a new DOM object.
                var task = this.InvokeCommand(command, document);
                this.tasks.Enqueue(task);
            }
            else
            {
                await this.InvokeCommand(command, document);
                context.InnerXml = document.XmlDocument.DocumentElement.InnerXml;
            }
        }

        private async Task InvokeCommand(ITaggerCommand command, IDocument document)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command), $"Command of type {command.GetType().FullName} is invalid.");
            }

            string message = command.GetDescriptionMessageForCommand();
            await InvokeProcessor(
                message,
                () => command.Run(document, this.settings),
                this.logger);
        }

        private IDocument WrapContextInDocument(XmlNode context)
        {
            var document = this.documentFactory.Create(Resources.ContextWrapper);
            document.XmlDocument.DocumentElement.InnerXml = context.InnerXml;
            document.SchemaType = this.settings.ArticleSchemaType;
            return document;
        }
    }
}
