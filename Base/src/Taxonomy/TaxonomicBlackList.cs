namespace ProcessingTools.Base.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class TaxonomicBlackList : IStringDataList
    {
        private static IEnumerable<string> stringList = null;

        private Config config;

        public TaxonomicBlackList(Config config)
        {
            this.config = config;
        }

        public IEnumerable<string> StringList
        {
            get
            {
                if (TaxonomicBlackList.stringList == null)
                {
                    XElement list = XElement.Load(this.config.blackListXmlFilePath);
                    TaxonomicBlackList.stringList = from item in list.Elements()
                                                    select item.Value;
                }

                return TaxonomicBlackList.stringList;
            }
        }

        public void Clear()
        {
            TaxonomicBlackList.stringList = null;
        }
    }
}
