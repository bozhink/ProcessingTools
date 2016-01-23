namespace ProcessingTools.Bio.Data.Miners.Models
{
    using Contracts;

    public class BiorepositoryInstitution : IBiorepositoryInstitution
    {
        public string Name { get; set; }

        public string Url { get; set; }
    }
}