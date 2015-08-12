namespace ProcessingTools.Base
{
    public class SpecimenCode
    {
        private string prefix;
        private string code;
        private string fullString;
        private string type;

        public SpecimenCode()
        {
            this.Prefix = null;
            this.Code = null;
            this.FullString = null;
        }

        public SpecimenCode(string prefix, string type, string code = null, string fullString = null)
        {
            this.Prefix = prefix;
            this.Type = type;
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

        public string Type
        {
            get
            {
                return this.type;
            }

            set
            {
                this.type = value;
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
