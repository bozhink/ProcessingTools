namespace ProcessingTools.Attributes
{
    using System;

    public class ValueAttribute : Attribute
    {
        public ValueAttribute(string value)
        {
            this.Value = value;
        }

        public string Value { get; set; }
    }
}