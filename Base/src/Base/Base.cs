using System;
using System.Xml;

namespace ProcessingTools.Base
{
    public abstract class Base : IBase
    {
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
                return this.XmlDocument.OuterXml;
            }

            set
            {
                if (value != null && value.Length > 0)
                {
                    try
                    {
                        this.xmlDocument.LoadXml(value);
                        this.NeedsUpdate = true;
                    }
                    catch (XmlException)
                    {
                        throw;
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
                        this.NeedsUpdate = true;
                    }
                    catch (XmlException)
                    {
                        throw;
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
        }

        private void Initialize(Config config)
        {
            Initialize();
            this.Config = config;
        }

        private void Initialize(Config config, string xml)
        {
            Initialize(config);
            this.Xml = xml;
            this.NeedsUpdate = true;
        }
    }
}
