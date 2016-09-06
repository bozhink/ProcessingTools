namespace ProcessingTools.Data.Common.Expressions.Contracts
{
    public interface IUpdateCommand
    {
        string FieldName { get; }

        string UpdateVerb { get; }

        object Value { get; }
    }
}
