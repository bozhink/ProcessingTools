namespace ProcessingTools.BaseLibrary.Floats.Contracts
{
    public interface IFloatObject
    {
        FloatsReferenceType FloatReferenceType { get; }

        string FloatTypeNameInLabel { get; }

        string MatchCitationPattern { get; }

        string InternalReferenceType { get; }

        string FloatObjectXPath { get; }
    }
}