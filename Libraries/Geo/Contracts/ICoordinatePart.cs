namespace ProcessingTools.Geo.Contracts
{
    public interface ICoordinatePart
    {
        string CoordinatePartString { get; set; }

        bool PartIsPresent { get; set; }

        string Type { get; }

        string Value { get; }

        void Parse();
    }
}
