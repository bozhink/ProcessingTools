namespace ProcessingTools.Bio.Services.Data.Models
{
    using ProcessingTools.Contracts.Models.Bio;

    public class MorphologicalEpithetServiceModel : IMorphologicalEpithet
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
