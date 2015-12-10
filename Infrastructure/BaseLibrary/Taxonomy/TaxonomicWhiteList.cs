namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using Configurator;
    using ProcessingTools.Contracts;

    public class TaxonomicWhiteList : IStringDataList
    {
        private static volatile IEnumerable<string> stringList = null;
        private static volatile Config config = null;

        public TaxonomicWhiteList(Config config)
        {
            object syncConfigLock = new object();
            if (TaxonomicWhiteList.config == null)
            {
                lock (syncConfigLock)
                {
                    if (TaxonomicWhiteList.config == null)
                    {
                        TaxonomicWhiteList.config = config;
                    }
                }
            }
        }

        public IEnumerable<string> StringList
        {
            get
            {
                object syncListLock = new object();
                if (TaxonomicWhiteList.stringList == null)
                {
                    lock (syncListLock)
                    {
                        if (TaxonomicWhiteList.stringList == null)
                        {
                            XElement list = XElement.Load(TaxonomicWhiteList.config.WhiteListXmlFilePath);
                            TaxonomicWhiteList.stringList = from item in list.Elements()
                                                            select item.Value;
                        }
                    }
                }

                return TaxonomicWhiteList.stringList;
            }
        }

        public void Clear()
        {
            object syncListLock = new object();
            lock (syncListLock)
            {
                TaxonomicWhiteList.stringList = null;
            }
        }
    }
}
