namespace ProcessingTools.Processors.Models.Bio.Codes
{
    public interface ISpecimenCode
    {
        string Code { get; }

        string FullString { get; }

        string Prefix { get; }

        string Type { get; }
    }
}
