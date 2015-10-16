namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Configurator;
    using Globals;

    public class TaxonomicBlackList : IStringDataList
    {
        private static volatile IEnumerable<string> stringList = null;
        private static volatile Config config = null;

        public TaxonomicBlackList(Config config)
        {
            object syncConfigLock = new object();
            if (TaxonomicBlackList.config == null)
            {
                lock (syncConfigLock)
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
                object syncListLock = new object();
                if (TaxonomicBlackList.stringList == null)
                {
                    lock (syncListLock)
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
            object syncListLock = new object();
            lock (syncListLock)
            {
                TaxonomicBlackList.stringList = null;
            }
        }
    }
}
