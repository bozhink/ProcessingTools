using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public class Environments : Base
    {
        public Environments()
            : base()
        {
        }

        public Environments(string xml)
            : base(xml)
        {
        }

        public Environments(Config config)
            : base(config)
        {
        }

        public void TagEnvironmentsRecords(List<EnvironmentRecord> environmentRecords)
        {
            xml = Format.Format.NormalizeNlmToSystemXml(config, xml);
            ParseXmlStringToXmlDocument();

            foreach (EnvironmentRecord env in environmentRecords.Cast<EnvironmentRecord>().ToArray().OrderBy(s => s.length))
            {
                //
            }





            if (config.NlmStyle)
            {
                xml = Format.Format.NormalizeSystemToNlmXml(config, xml);
            }
        }

        public struct EnvironmentRecord
        {
            // Identifier
            public string id;

            // ENVO id
            public string envoCode;

            // Link to groups
            public string link;

            // Main content of this environment
            public string content;

            // Length of the content string
            public int length;
        }
    }
}
