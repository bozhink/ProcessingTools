namespace ProcessingTools.Data.Common.Tests.Models
{
    using Attributes;

    public class ModelWithAttributeIdProperty
    {
        [CustomId]
        public int IndexProperty { get; set; }
    }
}