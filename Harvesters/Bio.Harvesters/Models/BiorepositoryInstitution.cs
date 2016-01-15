namespace ProcessingTools.Bio.Harvesters.Models
{
    using Contracts;

    public class BiorepositoryInstitution : IBiorepositoryInstitution
    {
        public string Name { get; set; }

        public string Url { get; set; }
    }
}