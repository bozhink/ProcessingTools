namespace ProcessingTools.Base
{
    using System;
    using System.Xml;

    /// <summary>
    /// Base object for all other xml-document-processing objects.
    /// </summary>
    public abstract class Base : IBase
    {
        /// <summary>
        /// XmlDocument object containing the xml content to be processed.
        /// </summary>
        private XmlDocument xmlDocument;

        /// <summary>
        /// Config object to be used as configuration settings provider.
        /// </summary>
        private Config config;

        /// <summary>
        /// The default namespace manager for the xmlDocument object.
        /// </summary>
        private XmlNamespaceManager namespaceManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Base"/> class with default null parameters.
        /// </summary>
        public Base()
        {
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Base"/> class with default null XmlDocument and a given Config.
        /// </summary>
        /// <param name="config">Config object to be used as configuration settings provider.</param>
        public Base(Config config)
        {
            this.Initialize(config);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Base"/> class with null Config and an XmlDocument with xml content.
        /// </summary>
        /// <param name="xml">XML content of the XmlDocument.</param>
        public Base(string xml)
        {
            this.Initialize(null, xml);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Base"/> class with given Config and XmlDocument content.
        /// </summary>
        /// <param name="config">Config object to be used as configuration settings provider.</param>
        /// <param name="xml">XML content of the XmlDocument.</param>
        public Base(Config config, string xml)
        {
            this.Initialize(config, xml);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Base"/> class copying content from the baseObject.
        /// </summary>
        /// <param name="baseObject">IBase object to provide content and settings for this new instance.</param>
        public Base(IBase baseObject)
        {
            this.Config = baseObject.Config;
            this.Xml = baseObject.Xml;
            this.XmlDocument = baseObject.XmlDocument;
            this.NeedsUpdate = true;
        }

        /// <summary>
        /// Gets or sets the XML content of the XmlDocument.
        /// </summary>
        /// <value>String with valid XML content.</value>
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

        /// <summary>
        /// Gets or sets the XmlDocument.
        /// </summary>
        /// <value>Non-null valid XmlDocument.</value>
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

        /// <summary>
        /// Gets the Config object which is the configuration settings provider.
        /// </summary>
        /// <value>The configuration settings provider of the class.</value>
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

        /// <summary>
        /// Gets the NamespaceManager of the Base object.
        /// </summary>
        /// <value>The NamespaceManager of the Base object.</value>
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
        /// Gets or sets a value indicating whether the child should update its content.
        /// </summary>
        /// <value>Should child update its content.</value>
        protected bool NeedsUpdate
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes the class with default null values for the Config and the XmlDocument.
        /// </summary>
        private void Initialize()
        {
            this.NamespaceManager = Config.TaxPubNamespceManager();
            this.xmlDocument = new XmlDocument(this.namespaceManager.NameTable);
            this.xmlDocument.PreserveWhitespace = true;
            this.config = null;
        }

        /// <summary>
        /// Initializes the class with default null setting for the XmlDocument and a given Config.
        /// </summary>
        /// <param name="config">Config object to be used as configuration settings provider.</param>
        private void Initialize(Config config)
        {
            this.Initialize();
            this.Config = config;
        }

        /// <summary>
        /// Initializes the class with a given XML content and Config object.
        /// </summary>
        /// <param name="config">Config object to be used as configuration settings provider.</param>
        /// <param name="xml">XML content of the XmlDocument.</param>
        private void Initialize(Config config, string xml)
        {
            this.Initialize(config);
            this.Xml = xml;
            this.NeedsUpdate = true;
        }
    }
}
