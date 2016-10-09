namespace ProcessingTools.Tagger
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Extensions;

    using Ninject;

    public partial class SingleFileProcessor
    {
        private Task InvokeProcessor(string message, Action action)
        {
            return Task.Run(() =>
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.logger?.Log(message);

                try
                {
                    action.Invoke();
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, string.Empty);
                }

                this.PrintElapsedTime(timer);
            });
        }

        private async Task InvokeProcessor<TController>(IKernel kernel)
            where TController : ITaggerController
        {
            await this.InvokeProcessor<TController>(this.document.XmlDocument.DocumentElement, kernel);
        }

        private async Task InvokeProcessor<TController>(XmlNode context, IKernel kernel)
            where TController : ITaggerController
        {
            var controller = kernel.Get<TController>();

            string message = controller.GetDescriptionMessageForController();

            await this.InvokeProcessor(
                message,
                () =>
                {
                    controller.Run(context, this.document.NamespaceManager, this.settings, this.logger).Wait();
                });
        }

        private async Task InvokeProcessor(Type controllerType, IKernel kernel)
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
                this.tasks.Enqueue(this.InvokeProcessor(controllerType, document.DocumentElement, kernel));
            }
            else
            {
                await this.InvokeProcessor(controllerType, this.document.XmlDocument.DocumentElement, kernel);
            }
        }

        private async Task InvokeProcessor(Type controllerType, XmlNode context, IKernel kernel)
        {
            var controller = kernel.Get(controllerType) as ITaggerController;

            string message = controller.GetDescriptionMessageForController();

            await this.InvokeProcessor(
                message,
                () =>
                {
                    controller.Run(context, this.document.NamespaceManager, this.settings, this.logger).Wait();
                });
        }
    }
}
