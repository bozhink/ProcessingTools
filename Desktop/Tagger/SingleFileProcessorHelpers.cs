namespace ProcessingTools.Tagger
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts.Controllers;
    using Core;
    using Extensions;

    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;

    public partial class SingleFileProcessor
    {
        private static async Task InvokeProcessor(string message, Func<object, Task> action, ILogger logger)
        {
            var timer = new Stopwatch();
            timer.Start();
            logger?.Log(message);

            try
            {
                await action.Invoke(null);
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

            PrintElapsedTime(timer, logger);
        }

        private static void PrintElapsedTime(Stopwatch timer, ILogger logger)
        {
            logger?.Log(LogType.Info, Messages.ElapsedTimeMessageFormat, timer.Elapsed);
        }

        private async Task InvokeProcessor<TController>(XmlNode context)
            where TController : ITaggerController
        {
            await this.InvokeProcessor(typeof(TController), context);
        }

        private async Task InvokeProcessor(Type controllerType, XmlNode context)
        {
            var controller = DI.Get(controllerType) as ITaggerController;
            var document = this.WrapContextInDocument(context);

            var isValidationController = controllerType.GetInterfaces().Count(t => t == typeof(INotAwaitableController)) > 0;
            if (isValidationController)
            {
                // Validation controllers should not overwrite the content of this.document.XmlDocument,
                // and here this content is copied in a new DOM object.
                var task = this.InvokeController(controller, document);
                this.tasks.Enqueue(task);
            }
            else
            {
                await this.InvokeController(controller, document);
                context.InnerXml = document.XmlDocument.DocumentElement.InnerXml;
            }
        }

        private async Task InvokeController(ITaggerController controller, IDocument document)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller), $"Controller of type {controller.GetType().FullName} is invalid.");
            }

            string message = controller.GetDescriptionMessageForController();
            await InvokeProcessor(
                message,
                _ => controller.Run(document, this.settings),
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
