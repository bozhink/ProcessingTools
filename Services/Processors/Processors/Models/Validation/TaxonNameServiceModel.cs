namespace ProcessingTools.Processors.Models.Validation
{
    using ProcessingTools.Constants.Uri;
    using ProcessingTools.Services.Validation.Contracts.Models;

    internal class TaxonNameServiceModel : ITaxonName
    {
        public string Name { get; set; }

        public string Permalink => $"{PermalinkPrefixes.ValidationCacheTaxonName}:{this.Name.Trim().ToLower()}";
    }
}
