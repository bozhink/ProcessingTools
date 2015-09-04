namespace ProcessingTools.Base.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class TaxonomicWhiteList : IStringDataList
    {
        private static volatile IEnumerable<string> stringList = null;
        private static object syncListLock = new object();

        private static volatile Config config = null;
        private static object syncConfigLock = new object();

        public TaxonomicWhiteList(Config config)
        {
            if (TaxonomicWhiteList.config == null)
            {
                lock (TaxonomicWhiteList.syncConfigLock)
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
                if (TaxonomicWhiteList.stringList == null)
                {
                    lock (TaxonomicWhiteList.syncListLock)
                    {
                        if (TaxonomicWhiteList.stringList == null)
                        {
                            XElement list = XElement.Load(TaxonomicWhiteList.config.whiteListXmlFilePath);
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
            lock (TaxonomicWhiteList.syncListLock)
            {
                TaxonomicWhiteList.stringList = null;
            }
        }
    }
}
