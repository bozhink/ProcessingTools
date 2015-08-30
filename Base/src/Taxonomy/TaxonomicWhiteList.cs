using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProcessingTools.Base.Taxonomy
{
    public class TaxonomicWhiteList : IStringDataList
    {
        private static IEnumerable<string> stringList = null;

        private Config config;

        public TaxonomicWhiteList(Config config)
        {
            this.config = config;
        }

        public IEnumerable<string> StringList
        {
            get
            {
                if (TaxonomicWhiteList.stringList == null)
                {
                    XElement list = XElement.Load(this.config.whiteListXmlFilePath);
                    TaxonomicWhiteList.stringList = from item in list.Elements()
                                                    select item.Value;
                }

                return TaxonomicWhiteList.stringList;
            }
        }

        public void Clear()
        {
            TaxonomicWhiteList.stringList = null;
        }
    }
}
