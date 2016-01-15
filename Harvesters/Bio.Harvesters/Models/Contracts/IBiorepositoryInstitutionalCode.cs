namespace ProcessingTools.Bio.Harvesters.Models.Contracts
{
    public interface IBiorepositoryInstitutionalCode
    {
        string InstitutionalCode { get; set; }

        string Description { get; set; }

        string Url { get; set; }
    }
}
