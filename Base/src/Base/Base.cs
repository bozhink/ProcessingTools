using System;
using System.Xml;

namespace ProcessingTools.Base
{
    public abstract class Base : IBase
    {
        private string xml;
        private XmlDocument xmlDocument;
        private Config config;
        private XmlNamespaceManager namespaceManager;

        public Base()
        {
            this.Initialize();
        }

        public Base(Config config)
        {
            this.Initialize(config);
        }

        public Base(string xml)
        {
            this.Initialize(null, xml);
        }

        public Base(Config config, string xml)
        {
            this.Initialize(config, xml);
        }

        public Base(IBase baseObject)
        {
            this.NamespaceManager = baseObject.NamespaceManager;
            this.Config = baseObject.Config;
            this.Xml = baseObject.Xml;
            this.XmlDocument = baseObject.XmlDocument;
            this.NeedsUpdate = true;
        }

        public string Xml
        {
            get
            {
                return this.xml;
            }

            set
            {
                if (value != null && value.Length > 0)
                {
                    try
                    {
                        this.xml = value;
                        this.xmlDocument.LoadXml(this.xml);
                        this.NeedsUpdate = true;
                    }
                    catch (XmlException e)
                    {
                        throw e;
                    }
                    catch (Exception e)
                    {
                        Alert.RaiseExceptionForType(e, this.GetType().Name, 51);
                    }
                }
                else
                {
                    throw new ArgumentNullException("Invalid value for Xml: null or empty.");
                }
            }
        }

        public XmlDocument XmlDocument
        {
            get
            {
                return this.xmlDocument;
            }

            set
            {
                if (value != null)
                {
                    try
                    {
                        this.xmlDocument = value;
                        this.xml = this.xmlDocument.OuterXml;
                        this.NeedsUpdate = true;
                    }
                    catch (XmlException e)
                    {
                        throw e;
                    }
                    catch (Exception e)
                    {
                        Alert.RaiseExceptionForType(e, this.GetType().Name, 51);
                    }
                }
                else
                {
                    throw new ArgumentNullException("Invalid value for XmlDocument: null.");
                }
            }
        }

        public Config Config
        {
            get
            {
                return this.config;
            }

            private set
            {
                this.config = value;
            }
        }

        public XmlNamespaceManager NamespaceManager
        {
            get
            {
                return this.namespaceManager;
            }

            private set
            {
                this.namespaceManager = value;
            }
        }

        /// <summary>
        /// Returns information if the child should update its content.
        /// </summary>
        protected bool NeedsUpdate
        {
            get;
            set;
        }

        private void Initialize()
        {
            this.NamespaceManager = ProcessingTools.Config.TaxPubNamespceManager();
            this.xmlDocument = new XmlDocument(this.namespaceManager.NameTable);
            this.xmlDocument.PreserveWhitespace = true;
            this.config = null;
            this.xml = null;
        }

        private void Initialize(Config config)
        {
            Initialize();
            this.Config = config;
        }

        private void Initialize(Config config, string xml)
        {
            Initialize();
            this.Config = config;
            this.Xml = xml; // This must not precede this.xmlDocument initialization
            this.NeedsUpdate = true;
        }
    }
}
