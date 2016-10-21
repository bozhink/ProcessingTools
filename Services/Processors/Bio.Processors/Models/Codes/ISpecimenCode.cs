namespace ProcessingTools.Bio.Processors.Models.Codes
{
    public interface ISpecimenCode
    {
        string Code { get; }

        string FullString { get; }

        string Prefix { get; }

        string Type { get; }
    }
}
