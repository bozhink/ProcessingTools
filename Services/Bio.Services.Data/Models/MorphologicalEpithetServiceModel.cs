namespace ProcessingTools.Bio.Services.Data.Models
{
    using Contracts;

    public class MorphologicalEpithetServiceModel : IMorphologicalEpithetServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}