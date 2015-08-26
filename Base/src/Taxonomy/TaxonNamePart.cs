//namespace ProcessingTools.Base.Taxonomy
//{
//    // TODO: remove this class
//    internal class TaxonNamePart
//    {
//        private string prefix, suffix;

//        public TaxonNamePart(bool taxPub = false)
//        {
//            if (taxPub)
//            {
//                this.Prefix = "<tp:taxon-name-part taxon-name-part-type=\"";
//                this.Suffix = "\">$1</tp:taxon-name-part>";
//            }
//            else
//            {
//                this.Prefix = "<tn-part type=\"";
//                this.Suffix = "\">$1</tn-part>";
//            }
//        }

//        public string Prefix
//        {
//            get
//            {
//                return this.prefix;
//            }

//            private set
//            {
//                this.prefix = value;
//            }
//        }

//        public string Suffix
//        {
//            get
//            {
//                return this.suffix;
//            }

//            private set
//            {
//                this.suffix = value;
//            }
//        }

//        public string TaxonNamePartReplace(string rank)
//        {
//            return this.Prefix + rank + this.Suffix;
//        }
//    }
//}
