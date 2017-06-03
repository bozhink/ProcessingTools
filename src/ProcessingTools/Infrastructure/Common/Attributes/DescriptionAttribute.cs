namespace ProcessingTools.Common.Attributes
{
    using System;

    public class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute(string description)
        {
            this.Description = description;
        }

        public string Description { get; set; }
    }
}
