namespace ProcessingTools.Base.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class TaxonomicBlackList : IStringDataList
    {
        private static volatile IEnumerable<string> stringList = null;
        private static object syncListLock = new object();

        private static volatile Config config = null;
        private static object syncConfigLock = new object();

        public TaxonomicBlackList(Config config)
        {
            if (TaxonomicBlackList.config == null)
            {
                lock (TaxonomicBlackList.syncConfigLock)
                {
                    if (TaxonomicBlackList.config == null)
                    {
                        TaxonomicBlackList.config = config;
                    }
                }
            }
        }

        public IEnumerable<string> StringList
        {
            get
            {
                if (TaxonomicBlackList.stringList == null)
                {
                    lock (TaxonomicBlackList.syncListLock)
                    {
                        if (TaxonomicBlackList.stringList == null)
                        {
                            XElement list = XElement.Load(TaxonomicBlackList.config.blackListXmlFilePath);
                            TaxonomicBlackList.stringList = from item in list.Elements()
                                                            select item.Value;
                        }
                    }
                }

                return TaxonomicBlackList.stringList;
            }
        }

        public void Clear()
        {
            lock (TaxonomicBlackList.syncListLock)
            {
                TaxonomicBlackList.stringList = null;
            }
        }
    }
}
