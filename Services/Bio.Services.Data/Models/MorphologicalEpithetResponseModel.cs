namespace ProcessingTools.Bio.Services.Data.Models
{
    using Contracts;

    public class MorphologicalEpithetResponseModel : IMorphologicalEpithet
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}