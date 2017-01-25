namespace ProcessingTools.Processors.Models.Validation
{
    using ProcessingTools.Services.Validation.Contracts.Models;

    internal class TaxonNameServiceModel : ITaxonName
    {
        public string Name { get; set; }
    }
}
