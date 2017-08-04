namespace ProcessingTools.Web.Documents.Areas.Data.ViewModels.CoordinatesCalculator
{
    using System.Collections.Generic;

    public class CoordinatesResponseViewModel : ICoordinates
    {
        private readonly ICollection<ICoordinate> coordinates;

        public CoordinatesResponseViewModel()
        {
            this.coordinates = new List<ICoordinate>();
        }

        public ICollection<ICoordinate> Coordinates => this.coordinates;
    }
}
