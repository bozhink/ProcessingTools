namespace ProcessingTools.Xml
{
    using System;
    using System.Xml;
    using Contracts;

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
