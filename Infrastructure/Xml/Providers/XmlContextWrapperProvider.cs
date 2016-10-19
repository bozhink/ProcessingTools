namespace ProcessingTools.Xml.Providers
{
    using System;
    using System.Xml;
    using Contracts.Providers;

    public class XmlContextWrapperProvider : IXmlContextWrapperProvider
    {
        public XmlDocument Create(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var wrapper = new XmlDocument
            {
                PreserveWhitespace = true
            };

            wrapper.LoadXml(Resources.ContextWrapper);
            wrapper.DocumentElement.InnerXml = context.InnerXml;

            return wrapper;
        }
    }
}
