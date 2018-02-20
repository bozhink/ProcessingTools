namespace ProcessingTools.Processors.Contracts.Geo.Coordinates
{
    using ProcessingTools.Processors.Models.Contracts.Geo.Coordinates;

    public interface ICoordinatesFactory
    {
        ICoordinate CreateCoordinate();

        ICoordinatePart CreateCoordinatePart();
    }
}
