namespace ProcessingTools.Bio.Biorepositories.Services.Data.Models
{
    using ProcessingTools.Services.Models.Contracts.Data.Bio.Biorepositories;

    public class Institution : IInstitution
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
