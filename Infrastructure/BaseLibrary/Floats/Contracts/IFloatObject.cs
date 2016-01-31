namespace ProcessingTools.BaseLibrary.Floats.Contracts
{
    public interface IFloatObject
    {
        FloatsReferenceType FloatReferenceType { get; }

        string FloatTypeNameInLabel { get; }

        string MatchCitationPattern { get; }

        string RefType { get; }

        string FloatObjectXPath { get; }
    }
}