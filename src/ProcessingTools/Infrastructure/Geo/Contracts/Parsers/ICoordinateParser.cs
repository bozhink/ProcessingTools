namespace ProcessingTools.Geo.Contracts.Parsers
{
    using Models;

    public interface ICoordinateParser
    {
        ICoordinate ParseCoordinateString(string coordinateString, string coordinateType = null);
    }
}
