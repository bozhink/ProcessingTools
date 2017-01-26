namespace ProcessingTools.Services.Validation.Models
{
    using Contracts.Models;
    using ProcessingTools.Constants.Uri;

    internal class TaxonNameServiceModel : ITaxonName
    {
        public string Name { get; set; }

        public string Permalink => $"{PermalinkPrefixes.ValidationCacheTaxonName}:{this.Name.Trim().ToLower()}";
    }
}
