namespace ProcessingTools.Processors.Models.References
{
    using Contracts.Models.References;

    internal class ReferenceTemplateItem : IReferenceTemplateItem
    {
        public string Id { get; set; }

        public string Year { get; set; }

        public string Authors { get; set; }
    }
}
