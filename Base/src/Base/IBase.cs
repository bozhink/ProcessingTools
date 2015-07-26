using System.Text;
using System.Xml;

namespace ProcessingTools.Base
{
    interface IBase
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
