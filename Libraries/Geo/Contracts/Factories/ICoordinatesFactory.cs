namespace ProcessingTools.Geo.Contracts.Factories
{
    using Models;

    public interface ICoordinatesFactory
    {
        ICoordinate CreateCoordinate();

        ICoordinatePart CreateCoordinatePart();
    }
}
