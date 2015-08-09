using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingTools.Base
{
    public class SpecimenCode
    {
        private string prefix;
        private string code;
        private string fullString;

        public SpecimenCode()
        {
            this.Prefix = null;
            this.Code = null;
            this.FullString = null;
        }

        public SpecimenCode(string prefix, string code = null, string fullString = null)
        {
            this.Prefix = prefix;
            this.Code = code;
            this.FullString = fullString;
        }

        public string Prefix
        {
            get
            {
                return this.prefix;
            }

            set
            {
                this.prefix = value;
            }
        }

        public string Code
        {
            get
            {
                return this.code;
            }

            set
            {
                this.code = value;
            }
        }

        public string FullString
        {
            get
            {
                return this.fullString;
            }

            set
            {
                this.fullString = value;
            }
        }
    }
}
