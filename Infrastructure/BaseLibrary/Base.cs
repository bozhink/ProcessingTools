namespace ProcessingTools.BaseLibrary
{
    using Contracts;

    using ProcessingTools.Configurator;
    using ProcessingTools.DocumentProvider;

    /// <summary>
    /// Base object for all other xml-document-processing objects.
    /// </summary>
    public abstract class Base : TaxPubDocument, IBase
    {
        /// <summary>
        /// Config object to be used as configuration settings provider.
        /// </summary>
        private Config config;

        /// <summary>
        /// Initializes a new instance of the <see cref="Base"/> class with null Config and an XmlDocument with xml content.
        /// </summary>
        /// <param name="xml">XML content of the XmlDocument.</param>
        public Base(string xml)
            : this(null, xml)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Base"/> class with given Config and XmlDocument content.
        /// </summary>
        /// <param name="config">Config object to be used as configuration settings provider.</param>
        /// <param name="xml">XML content of the XmlDocument.</param>
        public Base(Config config, string xml)
            : base(xml)
        {
            this.Config = config;
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
    }
}