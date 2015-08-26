namespace ProcessingTools.Base
{
    using System.Xml;

    public interface IBase
    {
        string Xml
        {
            get;
            set;
        }

        XmlDocument XmlDocument
        {
            get;
            set;
        }

        Config Config
        {
            get;
        }

        XmlNamespaceManager NamespaceManager
        {
            get;
        }
    }
}
