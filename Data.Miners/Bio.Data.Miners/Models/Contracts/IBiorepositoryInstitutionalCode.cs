namespace ProcessingTools.Bio.Data.Miners.Models.Contracts
{
    public interface IBiorepositoryInstitutionalCode
    {
        string InstitutionalCode { get; set; }

        string Description { get; set; }

        string Url { get; set; }
    }
}
