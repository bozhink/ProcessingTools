namespace ProcessingTools.Tagger
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
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

        private async Task InvokeProcessor<TController>()
            where TController : ITaggerController
        {
            await this.InvokeProcessor<TController>(this.document.XmlDocument.DocumentElement);
        }

        private async Task InvokeProcessor<TController>(XmlNode context)
            where TController : ITaggerController
        {
            var controller = DI.Get<TController>();

            await this.InvokeController(controller, context);
        }

        private async Task InvokeProcessor(Type controllerType)
        {
            // Do not wait validation controllers to return.
            var validationController = controllerType.GetInterfaces()?.FirstOrDefault(t => t == typeof(INotAwaitableController));
            if (validationController != null)
            {
                // Validation controllers should not overwrite the content of this.document.XmlDocument,
                // and here this content is copied in a new DOM object.
                XmlDocument document = new XmlDocument
                {
                    PreserveWhitespace = true
                };

                document.LoadXml(this.document.Xml);
                this.tasks.Enqueue(this.InvokeProcessor(controllerType, document.DocumentElement));
            }
            else
            {
                await this.InvokeProcessor(controllerType, this.document.XmlDocument.DocumentElement);
            }
        }

        private async Task InvokeProcessor(Type controllerType, XmlNode context)
        {
            var controller = DI.Get(controllerType) as ITaggerController;

            await this.InvokeController(controller, context);
        }

        private async Task InvokeController(ITaggerController controller, XmlNode context)
        {
            string message = controller.GetDescriptionMessageForController();

            var document = this.documentFactory.Create(Resources.ContextWrapper);
            document.XmlDocument.DocumentElement.InnerXml = context.InnerXml;
            document.SchemaType = this.settings.ArticleSchemaType;

            await InvokeProcessor(
                message,
                _ => controller.Run(document, this.settings),
                this.logger);

            context.InnerXml = document.XmlDocument.DocumentElement.InnerXml;
        }
    }
}
