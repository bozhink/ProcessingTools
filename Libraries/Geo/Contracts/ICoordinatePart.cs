namespace ProcessingTools.Geo.Contracts
{
    using Types;

    public interface ICoordinatePart
    {
        string CoordinatePartString { get; set; }

        double DecimalValue { get; set; }

        bool PartIsPresent { get; set; }

        CoordinatePartType Type { get; set; }

        string Value { get; }

        void Parse();
    }
}
