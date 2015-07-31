using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingTools.Base
{
    public class TagContent
    {
        private string name;

        public TagContent(string name = "", string attributes = "", string fullTag = "")
        {
            this.Name = name;
            this.Attributes = attributes;
            this.FullTag = fullTag;
        }

        public TagContent(TagContent tag)
        {
            this.Name = tag.Name;
            this.Attributes = tag.Attributes;
            this.FullTag = tag.FullTag;
        }

        public bool IsClosingTag
        {
            get;
            set;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.IsClosingTag = (this.name.Length > 0) ? ((this.name[0] == '/') ? true : false) : false;
            }
        }

        public string Attributes
        {
            get;
            set;
        }

        public string FullTag
        {
            get;
            set;
        }

        public string OpenTag
        {
            get
            {
                string openTag = "<" + this.Name;
                string attributes = this.Attributes;
                if (attributes != string.Empty && attributes != null)
                {
                    openTag += attributes;
                }

                openTag += ">";

                return openTag;
            }
        }

        public string CloseTag
        {
            get
            {
                string closeTag = "</" + this.Name + ">";
                return closeTag;
            }
        }
    }
}
