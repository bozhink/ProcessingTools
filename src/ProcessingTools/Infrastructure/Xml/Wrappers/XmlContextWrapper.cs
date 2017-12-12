namespace ProcessingTools.Xml.Wrappers
{
    using System;
    using System.Xml;
    using ProcessingTools.Contracts.Xml;

    public class XmlContextWrapper : IXmlContextWrapper
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
